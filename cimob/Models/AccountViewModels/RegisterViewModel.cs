using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View Register (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Número de utilizador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [BirthDateValidation(ErrorMessage = "A idade tem de ser entre 17 a 100 anos.")]
        [DataType(DataType.Date,ErrorMessage ="A data é inválida.")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Email que o utilizador utilizará para entrar na aplicação
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        /// <summary>
        /// Password que o utilizador utilizará para entrar na aplicação
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} a {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmação da Password que o utilizador utilizará para entrar na aplicação
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "A password e a password de confirmação não estão iguais.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}