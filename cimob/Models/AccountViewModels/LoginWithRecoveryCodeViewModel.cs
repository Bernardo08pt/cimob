using System.ComponentModel.DataAnnotations;

namespace cimob.Models.AccountViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View LoginWithRecoveryCode (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class LoginWithRecoveryCodeViewModel
    {
        /// <summary>
        /// Código para a recuperação da password
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
