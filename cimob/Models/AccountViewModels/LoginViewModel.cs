using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View Login (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email do utilizador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password do utilizador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Indica se o utilizador quer seja automáticamente autenticado
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}