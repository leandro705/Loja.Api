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

            CreateMap<Estado, EstadoDto>();
            CreateMap<EstadoDto, Estado>();

            CreateMap<Municipio, MunicipioDto>();
            CreateMap<MunicipioDto, Municipio>();

            CreateMap<Endereco, EnderecoDto>()
                .ForMember(d => d.EstadoId, dto => dto.MapFrom(s => s.Municipio.EstadoId));
            CreateMap<EnderecoDto, Endereco>();                

        }
    }
}
