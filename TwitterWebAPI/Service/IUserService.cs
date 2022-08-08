using TwitterWebAPI.Dtos;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public interface IUserService
    {
        Task<Response<List<GetUserDto>>> GetAllUsersAsync();
        Task<Response<List<GetUserDto>>> SearchUsersByNameAsync(string name);
    }
}
