using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using System;
using System.Globalization;
using System.Linq;

namespace Loja.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<UserDto, User>();

            CreateMap<Estado, EstadoDto>();
            CreateMap<EstadoDto, Estado>();

            CreateMap<Municipio, MunicipioDto>();
            CreateMap<MunicipioDto, Municipio>();

            CreateMap<Agendamento, AgendamentoDto>()
                .ForMember(d => d.DataAgendamentoStr, dto => dto.MapFrom(s => s.DataAgendamento.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.DataFinalAgendamentoStr, dto => dto.MapFrom(s => s.DataFinalAgendamento.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.ServicoNome, dto => dto.MapFrom(s => s.Servico.Nome))
                .ForMember(d => d.ServicoValor, dto => dto.MapFrom(s => s.Servico.Valor.ToString("N2", CultureInfo.CurrentCulture)))
                .ForMember(d => d.EstabelecimentoNome, dto => dto.MapFrom(s => s.Estabelecimento.Nome))
                .ForMember(d => d.UsuarioNome, dto => dto.MapFrom(s => s.Usuario.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.PossuiAtendimento, dto => dto.MapFrom(s => s.Atendimentos.Any(x => x.SituacaoId != (int)ESituacao.CANCELADO)));
            CreateMap<AgendamentoDto, Agendamento>()
                .ForMember(d => d.DataAgendamento, dto => dto.MapFrom(s => DateTime.Parse(s.DataAgendamentoStr)))
                .ForMember(d => d.DataFinalAgendamento, dto => dto.MapFrom(s => DateTime.Parse(s.DataFinalAgendamentoStr)));

            CreateMap<Atendimento, AtendimentoDto>()
                .ForMember(d => d.DataAtendimento, dto => dto.MapFrom(s => s.DataAtendimento.ToString("dd/MM/yyyy")))                
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))                
                .ForMember(d => d.EstabelecimentoNome, dto => dto.MapFrom(s => s.Estabelecimento.Nome))
                .ForMember(d => d.UsuarioNome, dto => dto.MapFrom(s => s.Usuario.Nome))
                .ForMember(d => d.ValorTotalFormatado, dto => dto.MapFrom(s => s.ValorTotal.ToString("N2", CultureInfo.CurrentCulture)))
                .ForMember(d => d.DescontoFormatado, dto => dto.MapFrom(s => s.Desconto.ToString("N2", CultureInfo.CurrentCulture)))
                .ForMember(d => d.ValorFormatado, dto => dto.MapFrom(s => s.Valor.ToString("N2", CultureInfo.CurrentCulture)))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<AtendimentoDto, Atendimento>()
                .ForMember(d => d.DataAtendimento, dto => dto.MapFrom(s => DateTime.Parse(s.DataAtendimento)));

            CreateMap<AtendimentoItem, AtendimentoItemDto>()
                .ForMember(d => d.ServicoNome, dto => dto.MapFrom(s => s.Servico.Nome))
                .ForMember(d => d.ValorFormatado, dto => dto.MapFrom(s => s.Valor.ToString("N2", CultureInfo.CurrentCulture)));
            CreateMap<AtendimentoItemDto, AtendimentoItem>();

            CreateMap<Estabelecimento, EstabelecimentoDto>()
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")));
            CreateMap<EstabelecimentoDto, Estabelecimento>();

            CreateMap<Servico, ServicoDto>()                
                .ForMember(d => d.Situacao, dto => dto.MapFrom(s => s.Situacao.Nome))
                .ForMember(d => d.EstabelecimentoNome, dto => dto.MapFrom(s => s.Estabelecimento.Nome))
                .ForMember(d => d.DataCadastro, dto => dto.MapFrom(s => s.DataCadastro.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(d => d.ValorFormatado, dto => dto.MapFrom(s => s.Valor.ToString("N2", CultureInfo.CurrentCulture)));
            CreateMap<ServicoDto, Servico>();

            CreateMap<Endereco, EnderecoDto>()
                .ForMember(d => d.EstadoId, dto => dto.MapFrom(s => s.Municipio.EstadoId));
            CreateMap<EnderecoDto, Endereco>();                

        }
    }
}
