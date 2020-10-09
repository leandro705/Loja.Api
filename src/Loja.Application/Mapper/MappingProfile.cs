using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using System;
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

            CreateMap<Agendamento, AgendamentoDto>()
                .ForMember(d => d.DataAgendamento, dto => dto.MapFrom(s => s.DataAgendamento.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.DataFinalAgendamento, dto => dto.MapFrom(s => s.DataFinalAgendamento.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.ServicoNome, dto => dto.MapFrom(s => s.Servico.Nome))
                .ForMember(d => d.EstabelecimentoNome, dto => dto.MapFrom(s => s.Estabelecimento.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<AgendamentoDto, Agendamento>()
                .ForMember(d => d.DataAgendamento, dto => dto.MapFrom(s => DateTime.Parse(s.DataAgendamento)))
                .ForMember(d => d.DataFinalAgendamento, dto => dto.MapFrom(s => DateTime.Parse(s.DataFinalAgendamento)));

            CreateMap<Atendimento, AtendimentoDto>();
            CreateMap<AtendimentoDto, Atendimento>();

            CreateMap<Estabelecimento, EstabelecimentoDto>()
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<EstabelecimentoDto, Estabelecimento>();

            CreateMap<Servico, ServicoDto>()                
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.EstabelecimentoNome, dto => dto.MapFrom(s => s.Estabelecimento.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.ValorFormatado, dto => dto.MapFrom(s => s.Valor.ToString("N", CultureInfo.CurrentCulture)));
            CreateMap<ServicoDto, Servico>();

            CreateMap<Endereco, EnderecoDto>()
                .ForMember(d => d.EstadoId, dto => dto.MapFrom(s => s.Municipio.EstadoId));
            CreateMap<EnderecoDto, Endereco>();                

        }
    }
}
