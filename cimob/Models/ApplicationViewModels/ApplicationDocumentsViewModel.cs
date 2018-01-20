using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ApplicationViewModels
{
    public class ApplicationDocumentsViewModel
    {
        public IDictionary<string, Ajuda> AjudasDictionary { get; internal set; }

        public List<CandidaturaDocumentos> DocumentosList { get; internal set; }

        public int CandidaturaID { get; set; }

        [Display(Name = "Novo documento")]
        public IFormFile Documento { get; set; }
    }
}
