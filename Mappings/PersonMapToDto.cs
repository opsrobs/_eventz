using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class PersonMapToDto : Profile
    {
        public PersonMapToDto()
        {
            CreateMap<PersonModel, PersonDto>().ReverseMap();
        }
    }
}
