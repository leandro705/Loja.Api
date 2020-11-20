using System;

namespace Loja.CrossCutting.Dto
{
    public class HorarioDisponivelDto
    {       
        public DateTime DataAgendamento { get; set; }        
        public string DataAgendamentoStr { get; set; }
        public string HorarioInicial { get; set; }
        public string HorarioFinal { get; set; }
    }
}
