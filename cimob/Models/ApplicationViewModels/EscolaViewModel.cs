using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class EscolaViewModel
    {
        public int Id { get; private set; }
        public Boolean Selected { get; set; }
        public string Pais { get; private set; }
        public string Escola { get; private set; }
        public string Curso { get; private set; }
    }
}