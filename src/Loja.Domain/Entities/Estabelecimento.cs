using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using System;
using System.Collections.Generic;

namespace Loja.Domain.Entities
{
    public class Estabelecimento
    {
        public int EstabelecimentoId { get; set; }    
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }

        public DateTime DataCadastro { get; set; }

        public int SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }
        public virtual IEnumerable<UserEstabelecimento> UserEstabelecimentos { get; set; }

        public void AtualizarEstabelecimento(EstabelecimentoDto estabelecimentoDto)
        {
            Nome = estabelecimentoDto.Nome;
            Email = estabelecimentoDto.Email;
            Descricao = estabelecimentoDto.Descricao;
            Telefone = estabelecimentoDto.Telefone;
            Celular = estabelecimentoDto.Celular;
            Instagram = estabelecimentoDto.Instagram;
            Facebook = estabelecimentoDto.Facebook;
        }

        public void DesabilitarEstabelecimento()
        {
            SituacaoId = (int)ESituacao.CANCELADO;
        }
    }
}
