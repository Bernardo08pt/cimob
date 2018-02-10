using System.Collections.Generic;

namespace cimob.Models.EscolasParceirasViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View Listagem de Escolas Parceiras (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class EscolasListViewModel
    {
        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }

        /// <summary>
        /// Listagem dos paises para a combobox de filtragem
        /// </summary>
        public List<Pais> PaisesList { get; set; }

        /// <summary>
        /// Listagem das escolas parceiras existentes
        /// </summary>
        public List<Escola> EscolasList { get; set; }
    }
}