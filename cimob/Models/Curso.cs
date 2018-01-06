namespace cimob.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public Escola EscolaID{ get; set; }
        public Pais Pais { get; set; }
        public int Vagas { get; set; }
    }
}