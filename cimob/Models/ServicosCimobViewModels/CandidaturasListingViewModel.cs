using System.Collections.Generic;

namespace cimob.Models.ServicosCimobViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View LoginWithRecoveryCode (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class CandidaturasListingViewModel
    {
        /// <summary>
        /// Listagem com todas as candidaturas existentes
        /// </summary>
        public List<Candidatura> Candidaturas { get; set; }
    }
}
