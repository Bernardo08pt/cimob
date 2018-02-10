using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.EditaisViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View da Candidatura do candidato (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class ApplicationViewModel
    {
        #region comboboxes
        /// <summary>
        /// Lista que popula a combobox Cursos do IPS
        /// </summary>
        public List<IpsCurso> CursoList { get; set; }
        /// <summary>
        /// Lista que popula a combobox Escolas do IPS
        /// </summary>
        public List<IpsEscola> EscolaList { get; set; }
        /// <summary>
        /// Lista que popula a combobox Parentesco
        /// </summary>
        public List<Parentesco> ParentescoList { get; set; }
        /// <summary>
        /// Lista que popula a combobox Escolas Parceiras e resptivos cursos
        /// </summary>
        public List<Escola> EscolasList { get; set; }
        /// <summary>
        /// Lista que popula a combobox Paises
        /// </summary>
        public List<Pais> PaisesList { get; set; }
        #endregion


        #region general 
        /// <summary>
        /// Número de candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Numero")]
        public int Numero { get; set; }

        /// <summary>
        /// Nome do Candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimento do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [BirthDateValidation(ErrorMessage = "A idade tem de ser entre 17 a 100 anos.")]
        [DataType(DataType.Date, ErrorMessage = "A data é inválida.")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Escola do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Escola")]
        public int Escola { get; set; }

        /// <summary>
        /// Curso do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Curso")]
        public int Curso { get; set; }

        /// <summary>
        /// Ano letivo atual
        /// </summary>
        [Display(Name = "Ano Letivo")]
        public string AnoLetivo { get; set; }
        #endregion


        #region contacts
        /// <summary>
        /// Email do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        /// <summary>
        /// Email alternativo do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email Alternativo")]
        public string EmailAlternativo { get; set; }

        /// <summary>
        /// Contacto pessoal do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Contacto Pessoal")]
        public string ContactoPessoal { get; set; }

        /// <summary>
        /// Contacto pessoal do emergência
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Contacto de Emergência")]
        public string ContactoEmergencia { get; set; }

        /// <summary>
        /// Parentesco do contacto de emergência do candidato
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+){1,1}", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Parentesco")]
        public int Parentesco { get; set; }
        #endregion


        #region docs
        /// <summary>
        /// Nome carta de motivação
        /// </summary>
        [Display(Name = "Carta de Motivação")]
        public IFormFile CartaMotivacao { get; set; }
        
        /// <summary>
        /// ID (na BD) do documento depois de carregado no servidor
        /// </summary>
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "O campo Carta de Motivação é obrigatório")]
        public int DocID { get; set; }
        #endregion


        #region destino
        /// <summary>
        /// Curssos (em JSON) escolhidos pelo candidato
        /// </summary>
        [Required(ErrorMessage = "É necessário escolher pelo menos um curso.")]
        public string SelectedCursos { get; set; }
        #endregion


        #region misc 
        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
        /// <summary>
        /// Listagem com erros
        /// </summary>
        public IDictionary<string, Erro> ErrosDictionary { get; set; }

        /// <summary>
        /// ID da mobilidade escolhida pelo candidato
        /// </summary>
        public int TipoMobilidade { get; set; }
        #endregion 
    }
}
