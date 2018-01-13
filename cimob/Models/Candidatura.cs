using System;
using System.Collections.Generic;

namespace cimob.Models
{
    public class Candidatura
    {
        public int CandidaturaID { get; set; }
        public string UtilizadorID { get; set; }
        public int TipoMobilidadeID{ get; set; }
        public int EstadoCandidaturaID { get; set; }
        public int EmegerenciaParentescoID { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Rejeitada { get; set; }
        public string RejeicaoRazao { get; set; }
        public string Entrevista { get; set; }
        public int Pontuacao { get; set; }
        public string Observacoes { get; set; }
        public string EmailAlternativo { get; set; }
        public string ContactoPessoal { get; set; }
        public string EmergenciaContacto { get; set; }
        public string AnoLetivo { get; set; }
        public int Semestre { get; set; }
        public int IpsCursoID { get; set; }
        public int Estagio { get; set; }
        public List<CandidaturaCursos> Cursos { get; set; }
        public List<CandidaturaDocumentos> Documentos { get; set; }
        
        public virtual ApplicationUser Utilizador { get; set; }
        public virtual TipoMobilidade TipoMobilidade { get; set; }
        public virtual EstadoCandidatura EstadoCandidatura { get; set; }
        public virtual Parentesco EmegerenciaParentesco { get; set; }
        public virtual IpsCurso IpsCurso { get; set; }
    }
}
