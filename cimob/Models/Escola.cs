using System.Collections.Generic;

namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela Escola na BD
    /// </summary>
    public class Escola
    {
        public int EscolaID { get; set; }
        public int TipoMobilidadeID { get; set; }
        public int PaisID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Curso> Cursos{ get; set; }
        public virtual Pais Pais { get; set; }
        public virtual TipoMobilidade TipoMobilidade{ get; set; }
    }
}