using System;

namespace cimob.Models
{
    public class Edital
    {
        public int EditalID { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public TipoMobilidade TipoMobilidade { get; set; }
        public DateTime DataLimite { get; set; }
    }
}
