using System.Collections.Generic;

namespace cimob.Models.ApplicationViewModels
{
    public class ApplicationDocumentsViewModel
    {
        public IDictionary<string, Ajuda> AjudasDictionary { get; internal set; }

        public List<CandidaturaDocumentos> Documentos { get; internal set; }
    }
}
