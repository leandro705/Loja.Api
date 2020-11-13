using Loja.CrossCutting.Enumerators;
using System.Collections.Generic;

namespace Loja.CrossCutting.Dto
{
    public class AtendimentoDto
    {
        public int AtendimentoId { get; set; }
        public string DataAtendimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string ValorTotalFormatado { get; set; }
        public string DescontoFormatado { get; set; }
        public string ValorFormatado { get; set; }
        public string Observacao { get; set; }        
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }

        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }
        public int AgendamentoId { get; set; }
        public IEnumerable<AtendimentoItemDto> AtendimentoItens { get; set; }
    }
}
