using System;
using System.Collections.Generic;

namespace cimob.Models
{
    public class Candidatura
    {
        public int CandidaturaID { get; set; }
        public Utilizador Utilizador { get; set; }
        public TipoMobilidade TipoMobilidade{ get; set; }
        public List<Curso> Cursos { get; set; }
        public EstadoCandidatura EstadoCandidatura { get; set; }
        public Parentesco EmegerenciaParentesco { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Rejeitada { get; set; }
        public string RejeicaoRazao { get; set; }
        public string Entrevista { get; set; }
        public int Pontuacao { get; set; }
        public string Observacoes { get; set; }
        public string EmailAlternativo { get; set; }
        public int ContactoPessoal { get; set; }
        public int EmergenciaContacto { get; set; }
        public string AnoLetivo { get; set; }
        public int Semestre { get; set; }
        public IpsCurso IpsCurso { get; set; }
        public int Estagio { get; set; }
        public List<Documento> Documentos { get; set; }
    }
}
