using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class UserMapToDto : Profile
    {
        public UserMapToDto() 
        {
            CreateMap<UserModel,UserDto>().ReverseMap();
        }
    }
}
