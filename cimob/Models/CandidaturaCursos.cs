namespace cimob.Models
{
    public class CandidaturaCursos
    {
        public int CandidaturaCursosID { get; set; }
        public int CandidaturaID { get; set; }
        public int CursoID { get; set; }

        public virtual Candidatura Candidatura { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
