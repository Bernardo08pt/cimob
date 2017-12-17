using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}