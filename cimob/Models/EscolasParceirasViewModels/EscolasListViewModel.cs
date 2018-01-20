using System.Collections.Generic;

namespace cimob.Models.EscolasParceirasViewModels
{
    public class EscolasListViewModel
    {
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
        public List<Pais> PaisesList { get; set; }
        public List<Escola> EscolasList { get; set; }
    }
}