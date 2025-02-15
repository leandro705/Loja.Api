﻿using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using System;
using System.Collections.Generic;

namespace Loja.Domain.Entities
{
    public class Agendamento
    {
        public int AgendamentoId { get; set; }        
        public DateTime DataAgendamento { get; set; }
        public DateTime DataFinalAgendamento { get; set; }        
        public string Observacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public int ServicoId { get; set; }
        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual User Usuario { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }
        
        public int SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }

        public virtual IEnumerable<Atendimento> Atendimentos{ get; set; } = new List<Atendimento>();

        public void AtualizarAgendamento(AgendamentoDto agendamentoDto)
        {
            DataAgendamento = DateTime.Parse(agendamentoDto.DataAgendamentoStr);
            DataFinalAgendamento = DateTime.Parse(agendamentoDto.DataFinalAgendamentoStr);
            Observacao = agendamentoDto.Observacao;
            ServicoId = agendamentoDto.ServicoId;
            UserId = agendamentoDto.UserId;
            EstabelecimentoId = agendamentoDto.EstabelecimentoId;
        }

        public void FinalizarAgendamento()
        {
            SituacaoId = (int)ESituacao.FINALIZADO;
        }

        public void CancelarAgendamento()
        {
            SituacaoId = (int)ESituacao.CANCELADO;
        }

        public bool VerificaAgendamentoDuplicado(int estabelecimentoId, int agendamentoId, DateTime dataAgendamento, DateTime dataFinalAgendamento)
        {
            return EstabelecimentoId == estabelecimentoId && SituacaoId != (int)ESituacao.CANCELADO &&
                    ((agendamentoId > 0 && AgendamentoId != agendamentoId) || agendamentoId == 0) &&
                    ((dataAgendamento >= DataAgendamento && dataAgendamento < DataFinalAgendamento) ||
                    (dataFinalAgendamento > DataAgendamento && dataFinalAgendamento <= DataFinalAgendamento));

        }
    }
}
