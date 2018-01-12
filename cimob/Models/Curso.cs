namespace cimob.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public int PaisID { get; set; }
        public int EscolaID { get; set; }
        public int Vagas { get; set; }
        public string Nome { get; set; }

        public virtual Pais Pais { get; set; }
        public virtual Escola Escola { get; set; }
    }
}