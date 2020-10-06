using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using System.Globalization;

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

            CreateMap<Agendamento, AgendamentoDto>();
            CreateMap<AgendamentoDto, Agendamento>();

            CreateMap<Atendimento, AtendimentoDto>();
            CreateMap<AtendimentoDto, Atendimento>();

            CreateMap<Estabelecimento, EstabelecimentoDto>()
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<EstabelecimentoDto, Estabelecimento>();

            CreateMap<Servico, ServicoDto>()
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.ValorFormatado, dto => dto.MapFrom(s => s.Valor.ToString("N", CultureInfo.CurrentCulture)));
            CreateMap<ServicoDto, Servico>();

            CreateMap<Endereco, EnderecoDto>()
                .ForMember(d => d.EstadoId, dto => dto.MapFrom(s => s.Municipio.EstadoId));
            CreateMap<EnderecoDto, Endereco>();                

        }
    }
}
