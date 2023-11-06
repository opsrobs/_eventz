﻿using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class UserMapCreateMapping : Profile
    {
        public UserMapCreateMapping()
        {
            CreateMap<PersonToDtoCreate, UserModel>()
                .ForPath(dest => dest.Person.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.Person.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Person.Password, opt => opt.MapFrom(src => src.Password))
                .ForPath(dest => dest.Person.Roles, opt => opt.MapFrom(src => src.Roles));
        }

    }
}
