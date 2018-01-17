using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models
{
    public class CursosViewModel
    {
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Vagas")]
        public int Vagas { get; set; }
    }
}
