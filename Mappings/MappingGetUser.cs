using AutoMapper;
using eventz.DTOs;
using eventz.Models;

namespace eventz.Mappings
{
    public class MappingGetUser : Profile
    {
        public MappingGetUser()
        {
            CreateMap<UserModel, UserToDtoList>();
        }
    }
}
