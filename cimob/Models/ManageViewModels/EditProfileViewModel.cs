using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ManageViewModels
{
    public class EditProfileViewModel
    {
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} a {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} a {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("NewPassword", ErrorMessage = "A password e a password de confirmação não estão iguais.")]
        public string ConfirmPassword { get; set; }



    }
}
