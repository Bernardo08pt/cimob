namespace cimob.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nome{ get; set; }
        public Pais Pais { get; set; }
        public int Vagas { get; set; }
    }
}