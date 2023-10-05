using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class MappingUserToUpdate : Profile
    {
        public MappingUserToUpdate()
        {
            CreateMap<PersonToDtoUpdate, PersonModel>();

            CreateMap<UserToDtoUpdate, UserModel>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => src.PersonID));

        }

    }
}
