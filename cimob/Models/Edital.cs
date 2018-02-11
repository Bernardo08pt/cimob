using System;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela Editais na BD
    /// </summary>
    public class Edital
    {
        public int EditalID { get; set; }
        public int TipoMobilidadeID { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string NomeFicheiro { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataLimite { get; set; }

        //Adicionado
        public int Estado { get; set; }

        public virtual TipoMobilidade TipoMobilidade { get; set; }
    }
}
