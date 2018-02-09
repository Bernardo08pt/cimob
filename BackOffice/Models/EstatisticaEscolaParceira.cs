using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    public class EstatisticaEscolaParceira
    {
        //Nome da escola parceira
        public string Nome { get; set; }
        //Contagem de pedidos para outgoing
        public int Contagem { get; set; }
    }
}
