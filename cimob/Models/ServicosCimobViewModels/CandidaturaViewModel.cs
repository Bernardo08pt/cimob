using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ServicosCimobViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View de visualização de Candidatura pelo CIMOB
    /// (quaisquer campos, quer preenchiveis pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class CandidaturaViewModel
    {
        /// <summary>
        /// ID da candidatura a ser visualizada
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID do novo documento carregado (usado depois de carregar um documento novo
        /// para associar o documento à candidatura)
        /// </summary>
        public int DocID { get; set; }

        #region geral 
        /// <summary>
        /// Nome do candidato
        /// </summary>
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Número do candiato
        /// </summary>
        [Display(Name = "Número")]
        public int Numero { get; set; }

        /// <summary>
        /// Email do candidato
        /// </summary>
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Email alternativo do candidato
        /// </summary>
        [Display(Name = "Email alternativo")]
        public string EmailAlternativo { get; set; }

        /// <summary>
        /// Escola do candidato
        /// </summary>
        [Display(Name = "Escola")]
        public string Escola { get; set; }
        
        /// <summary>
        /// Curso do candidato
        /// </summary>
        [Display(Name = "Curso")]
        public string Curso { get; set; }

        /// <summary>
        /// Ano letivo de quando a candidatura foi submetida
        /// </summary>
        [Display(Name = "Ano Letivo")]
        public string AnoLetivo { get; set; }
                
        /// <summary>
        /// Contacto pessoal do candidato
        /// </summary>
        [Display(Name = "Contacto pessoal")]
        public string ContactoPessoal { get; set; }
        
        /// <summary>
        /// Contacto de urgência do candidato
        /// </summary>
        [Display(Name = "Contacto de urgência")]
        public string ContactoEmergencia { get; set; }

        /// <summary>
        /// Parentesco do contacto de urgência do candidato
        /// </summary>
        [Display(Name = "Parentesco (contacto)")]
        public string Parentesco { get; set; }
        #endregion

        #region destino
        /// <summary>
        /// Nome da escola para onde o candidato quer ir
        /// </summary>
        [Display(Name = "Nome escola")]
        public string NomeEscola { get; set; }

        /// <summary>
        /// País da escola para onde o candidato quer ir
        /// </summary>
        [Display(Name = "País")]
        public string Pais { get; set; }

        /// <summary>
        /// Cursos que o candidato escolheu
        /// </summary>
        [Display(Name = "Cursos")]
        public Dictionary<int, Dictionary<string, int>> CursoDestino { get; set; }
        #endregion

        #region documentos
        /// <summary>
        /// Novo documento a ser inserido
        /// </summary>
        [Display(Name = "Novos Documentos")]
        public IFormFile NovoDocumento { get; set; }

        /// <summary>
        /// Listagem de todos os documentos associados à candidatura
        /// </summary>
        public List<Documento> Documentos { get; set; }
        #endregion

        #region avaliacao
        /// <summary>
        /// Data da entrevista
        /// </summary>
        [Display(Name = "Entrevista")]
        public string Entrevista { get; set; }

        /// <summary>
        /// Pontuação da candidatura
        /// </summary>
        [Display(Name = "Pontuação")]
        public int Pontuacao { get; set; }

        /// <summary>
        /// Observações extra à candidatura
        /// </summary>
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        /// <summary>
        /// Listagem de avaliação de UCs
        /// </summary>
        [Display(Name = "Listagem")]
        public List<UCAvaliacao> Listagem { get; set; }

        /// <summary>
        /// Listagem de novas de avaliações de UCs adicionados (em JSON)
        /// </summary>
        public List<string> NovaListagem { get; set; }

        /// <summary>
        /// Motivo da rejeição da candidatura
        /// </summary>
        [Display(Name = "Razão")]
        public string Razao{ get; set; }
        
        /// <summary>
        /// Estado atual da candidatura
        /// </summary>
        public int Estado { get; set; }

        /// <summary>
        /// Nome da UCAvaliacao a adicionar
        /// </summary>
        [Display(Name = "Unidade Curricular")]
        public string UC { get; set; }
        
        /// <summary>
        /// Critério da UCAvaliacao a adicionar
        /// </summary>
        [Display(Name = "Critério")]
        public string Criterio { get; set; }
        #endregion

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}