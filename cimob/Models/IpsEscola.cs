using System.Collections.Generic;

namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela IpsEscolas na BD
    /// </summary>
    public class IpsEscola
    {
        public int IpsEscolaID { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<IpsCurso> Cursos { get; set; }
    }
}