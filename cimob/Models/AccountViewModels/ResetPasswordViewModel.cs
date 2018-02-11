using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View ResetPassword (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email do utilizador cuja password vai ser alterada
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Nova passwords
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} a {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmação de nova password
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "A password e a password de confirmação não estão iguais.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Código de segurança que garante que é o utilizador certo a alterar a password
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
