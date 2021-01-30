using Loja.CrossCutting.Dto;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Loja.Test.Builder.Dto
{
    public class AtendimentoDtoBuilder
    {
        private static int nextId;

        public int AtendimentoId { get; set; }
        public string DataAtendimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }       
        public string Observacao { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }
     
        public int SituacaoId { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }
        public int? AgendamentoId { get; set; }
        public List<AtendimentoItemDto> AtendimentoItens { get; set; } = new List<AtendimentoItemDto>();

        public AtendimentoDtoBuilder()
        {           
            var estabelecimentoDtoBuilder = new EstabelecimentoDtoBuilder().ComBarbearia().Build();
            var usuarioDtoBuilder = new UsuarioDtoBuilder().ComCliente1().Build();
            var atendimentoItemDtoBuilder = new AtendimentoItemDtoBuilder().Build();            

            AtendimentoId = Interlocked.Increment(ref nextId);
            DataAtendimento = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year + " 10:00";
            Valor = 30;
            Desconto = 0;
            ValorTotal = 30;

            UserId = usuarioDtoBuilder.Id;
            UsuarioNome = usuarioDtoBuilder.Nome;

            EstabelecimentoId = estabelecimentoDtoBuilder.EstabelecimentoId;
            EstabelecimentoNome = estabelecimentoDtoBuilder.Nome;
            AtendimentoItens.Add(atendimentoItemDtoBuilder);

        }

        public AtendimentoDto Build() => new AtendimentoDto()
        {
            AtendimentoId = AtendimentoId,
            DataAtendimento = DataAtendimento,
            Valor = Valor,
            Desconto = Desconto,
            ValorTotal = ValorTotal,            
            UserId = UserId,
            UsuarioNome = UsuarioNome, 
            EstabelecimentoId = EstabelecimentoId,
            EstabelecimentoNome = EstabelecimentoNome,
            AgendamentoId = AgendamentoId,
            AtendimentoItens = AtendimentoItens
        };

        public AtendimentoDtoBuilder ComID(int ID)
        {
            AtendimentoId = ID;
            return this;
        }

        public AtendimentoDtoBuilder ComAgendamentoID(int ID)
        {
            AgendamentoId = ID;
            return this;
        }
    }
}
