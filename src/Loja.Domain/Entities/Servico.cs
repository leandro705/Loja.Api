using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using System;

namespace Loja.Domain.Entities
{
    public class Servico
    {
        public int ServicoId { get; set; } 
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Duracao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int SituacaoId { get; set; }
        public int EstabelecimentoId { get; set; }

        public virtual Situacao Situacao { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }

        public void AtualizarServico(ServicoDto servicoDto)
        {
            Nome = servicoDto.Nome;
            Valor = servicoDto.Valor;
            Duracao = servicoDto.Duracao;
            EstabelecimentoId = servicoDto.EstabelecimentoId;
        }

        public void DesabilitarServico()
        {
            SituacaoId = (int)ESituacao.CANCELADO;
        }
    }
}
