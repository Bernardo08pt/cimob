using System.Collections.Generic;

namespace cimob.Models
{
    public class Escola
    {
        public int EscolaID { get; set; }
        public TipoMobilidade TipoMobilidade { get; set; }
        public Pais Pais { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Estado { get; set; }
        public List<Curso> Cursos{ get; set; }
    }
}
