namespace Loja.Domain.Entities
{
    public class Municipio
    {
        public int MunicipioId { get; set; }        
        public string Nome { get; set; }
        public string UF { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

    }
}
