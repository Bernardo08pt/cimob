using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View ForgotPassword (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email do utilizador que é utilizado para encontrar
        /// o utilizador na BD e fazer "reset" à sua password
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
