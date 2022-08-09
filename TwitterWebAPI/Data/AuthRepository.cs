using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using TwitterWebAPI.Models;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Utilities;

namespace TwitterWebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<Response<int>> RegisterAsync(User user, string password)
        {
            Response<int> response = new Response<int>();

            CreateHashPassword(password, out byte[] hashPassword, out byte[] saltPassword);

            user.HashPassword = hashPassword;
            user.SaltPassword = saltPassword;
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            response.Result = user.Id;
            response.Message = "User registered succesfully";

            return response;
        }

        public async Task<Response<string>> LoginAsync(string username, string password)
        {
            var response = new Response<string>();
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyHashPassword(password, user.HashPassword, user.SaltPassword))
            {
                response.Success = false;
                response.Message = "Password didn't match";
            }
            else
            {
                response.Result = CreateJWTToken(user);
            }
            return response;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyHashPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.LoginId)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("TweetSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Response<string>> ForgotPasswordAsync(string loginId)
        {
            var response = new Response<string>();

            string newPassword = RandomPassword.GenerateRandomPassword(15);

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower().Equals(loginId.ToLower()));
            if (user != null)
            {
                CreateHashPassword(newPassword, out byte[] hashPassword, out byte[] saltPassword);

                user.HashPassword = hashPassword;
                user.SaltPassword = saltPassword;

                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                response.Result = newPassword;
                response.Message = "New password created succesfully";
            }

            return response;
        }

    }
}
