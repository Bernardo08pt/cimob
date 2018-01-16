using System;

namespace cimob.Models
{
    public class Edital
    {
        public int EditalID { get; set; }
        public int TipoMobilidadeID { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string NomeFicheiro { get; set; }
        public DateTime DataLimite { get; set; }

        //Adicionado
        public int Estado { get; set; }

        public virtual TipoMobilidade TipoMobilidade { get; set; }
    }
}
