using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ServicosCimobViewModels
{
    public class CandidaturaViewModel
    {
        public int ID { get; set; }
        public int DocID { get; set; }

        #region geral 
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nº de aluno")]
        public int Numero { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Email alternativo")]
        public string EmailAlternativo { get; set; }

        [Display(Name = "Escola")]
        public string Escola { get; set; }
        
        [Display(Name = "Curso")]
        public string Curso { get; set; }

        [Display(Name = "Ano Letivo")]
        public string AnoLetivo { get; set; }
                
        [Display(Name = "Contacto pessoal")]
        public string ContactoPessoal { get; set; }
        
        [Display(Name = "Contacto de urgência")]
        public string ContactoEmergencia { get; set; }
        
        [Display(Name = "Parentesco (contacto)")]
        public string Parentesco { get; set; }
        #endregion

        #region destino
        [Display(Name = "Nome escola")]
        public string NomeEscola { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }

        [Display(Name = "Cursos")]
        public Dictionary<int, Dictionary<string, int>> CursoDestino { get; set; }
        #endregion

        #region documentos
        [Display(Name = "Novos Documentos")]
        public IFormFile NovoDocumento { get; set; }

        public List<Documento> Documentos { get; set; }
        #endregion

        #region avaliacao
        [Display(Name = "Entrevista")]
        public string Entrevista { get; set; }

        [Display(Name = "Pontuação")]
        public int Pontuacao { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Listagem")]
        public List<UCAvaliacao> Listagem { get; set; }

        public List<string> NovaListagem { get; set; }

        [Display(Name = "Razão")]
        public string Razao{ get; set; }
        
        public int Estado { get; set; }
        #endregion

        [Display(Name = "Unidade Curricular")]
        public string UC { get; set; }
        [Display(Name = "Critério")]
        public string Criterio { get; set; }

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
