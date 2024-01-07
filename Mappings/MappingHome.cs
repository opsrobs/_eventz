using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class MappingHome : Profile
    {
        public MappingHome()
        {
            CreateMap<HomeDto, Home>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections));

            CreateMap<SectionDtoRequest, Section>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora Id, pois ele não está presente em SectionDtoRequest
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.SectionName))
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events));

            CreateMap<UserModel, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth));

            CreateMap<PersonToDtoCreate, UserModel>()
                .ForPath(dest => dest.Person.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.Person.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Person.Password, opt => opt.MapFrom(src => src.Password))
                .ForPath(dest => dest.Person.Roles, opt => opt.MapFrom(src => src.Roles));

            CreateMap<UserToDtoUpdate, UserModel>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));

            CreateMap<CreateUserRequestDto, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));

            CreateMap<PersonModel, PersonDto>().ReverseMap();

            CreateMap<PersonToDtoUpdate, PersonModel>();

            CreateMap<UserModel, UserToDtoList>();
            CreateMap<EventDtoRequest, Event>();

            CreateMap<EventDtoRequest, Event>()
                .ForMember(dest => dest.ThisLocalization, opt => opt.MapFrom(src => src.ThisLocalization));

            CreateMap<LocalizationDto, Localization>();


            CreateMap<Category, CategoryDtoRequest>();


        }
    }
}
