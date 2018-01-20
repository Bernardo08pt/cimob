using System;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models
{
    public class Documento
    {
        public int DocumentoID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataUpload { get; set; }
        public string FicheiroNome { get; set; }
        public string FicheiroCaminho{ get; set; }
        public int OrigemCimob{ get; set; }
    }
}