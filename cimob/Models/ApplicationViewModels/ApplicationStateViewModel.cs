using System.Collections.Generic;

namespace cimob.Models.EditaisViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View State (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class ApplicationStateViewModel
    {
        /// <summary>
        /// Listagem com os possiveis estados da candidatura
        /// </summary>
        public List<EstadoCandidatura> EstadosList { get; internal set; }

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; internal set; }

        /// <summary>
        /// Estado atual da candidatura
        /// </summary>
        public int Estado { get; set; }
    }
}
