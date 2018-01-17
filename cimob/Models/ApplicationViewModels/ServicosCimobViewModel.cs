using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class ServicosCimobViewModel
    {
        #region geral 
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nº de aluno")]
        public string Numero { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Email alternativo")]
        public string EmailAlternativo { get; set; }

        [Display(Name = "Escola")]
        public List<IpsEscola> Escola { get; set; }
        
        [Display(Name = "Curso")]
        public IpsCurso Curso { get; set; }

        [Display(Name = "Ano")]
        public int Ano { get; set; }
                
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

        [Display(Name = "Curso ")]
        public string CursoDestino { get; set; }

        [Display(Name = "Vagas disponívei: ")]
        public int VagasDisponiveis { get; set; }
        #endregion

        #region documentos
        [Display(Name = "Novos Documentos")]
        public string NovosDocumentos { get; set; }

        public List<Documento> Documentos { get; set; }
        #endregion

        #region avaliacao
        [Display(Name = "Entrevista")]
        public string Entrevista { get; set; }

        [Display(Name = "Pontuação")]
        public string Pontuacao { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Listagem")]
        public string Listagem { get; set; } //Será uma lista com as unidades curriculares??
        #endregion

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }

    }
}
