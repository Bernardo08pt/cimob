using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class EditaisViewModel
    {
        public List<Edital> Editais;
        public SelectList MobilityPrograms;
        public string MobilityProgram;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Carregar edital:")]
        public IFormFile CarregarEdital { get; set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
