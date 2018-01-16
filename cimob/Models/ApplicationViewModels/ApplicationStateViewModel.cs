using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class ApplicationStateViewModel
    {
        public List<EstadoCandidatura> EstadosList { get; internal set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; internal set; }

        public int Estado { get; set; }
    }
}
