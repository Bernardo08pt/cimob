using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class EscolaViewModel
    {
        public int Id { get; private set; }
        public List<Escola> Escolas { get; set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}