using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class UserMapToDto : Profile
    {
        public UserMapToDto()
        {
            CreateMap<UserModel, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth));
        }
    }
}
