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
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.PersonID.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PersonID.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PersonID.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PersonID.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth));
        }
    }
}
