using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Data;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        public readonly IMapper _mapper;

        public UserService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<GetUserDto>>> GetAllUsersAsync()
        {
            var response = new Response<List<GetUserDto>>();
            var user = await _appDbContext.Users.ToListAsync();
            response.Result = user.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return response;
        }

       public async Task<Response<List<GetUserDto>>> SearchUsersByNameAsync(string name)
        {
            var response = new Response<List<GetUserDto>>();
            var user = await _appDbContext.Users.Where(u => u.LoginId.Contains(name)).ToListAsync();
            response.Result = user.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return response;
        }
    }
}
