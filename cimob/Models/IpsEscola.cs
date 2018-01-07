using System.Collections.Generic;

namespace cimob.Models
{
    public class IpsEscola
    {
        public int IpsEscolaID { get; set; }
        public string Descricao { get; set; }
        public List<IpsCurso> Cursos { get; set; }
    }
}