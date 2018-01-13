namespace cimob.Models
{
    public class IpsCurso
    {
        public int IpsCursoID { get; set; }
        public int IpsEscolaID { get; set; }
        public string Nome { get; set; }

        public virtual IpsEscola IpsEscola { get; set; }
    }
}
