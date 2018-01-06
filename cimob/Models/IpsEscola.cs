using cimob.Models;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public class IpsEscola
    {
        public int IpsEscolaID { get; set; }
        public string Descricao { get; set; }
        public List<IpsCurso> Cursos { get; set; }
    }
}
