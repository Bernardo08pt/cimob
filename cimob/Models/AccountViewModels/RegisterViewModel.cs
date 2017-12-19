using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [BirthDateValidation(ErrorMessage = "O candidato tem de ter pelo menos 17 anos.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} a {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "A password e a password de confirmação não estão iguais.")]
        public string ConfirmPassword { get; set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }

        

    }




}