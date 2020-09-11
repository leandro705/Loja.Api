using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;

namespace Loja.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
