using AutoMapper;
using TwitterWebAPI.Dtos;
using TwitterWebAPI.Models;

namespace TwitterWebAPI
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<User, GetUserDto>();
        }
    }
}
