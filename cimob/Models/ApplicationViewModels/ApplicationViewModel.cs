using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ApplicationViewModels
{
    public class ApplicationViewModel
    {
        public int TipoMobilidade { get; set; }
        
        #region comboboxes
        public List<IpsCurso> CursoList { get; set; }
        public List<IpsEscola> EscolaList { get; set; }
        public List<Parentesco> ParentescoList { get; set; }
        public List<Escola> EscolasList { get; set; }
        public List<Pais> PaisesList { get; set; }
        #endregion


        #region general 
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Numero")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [BirthDateValidation(ErrorMessage = "A idade tem de ser entre 17 a 100 anos.")]
        [DataType(DataType.Date, ErrorMessage = "A data é inválida.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Escola")]
        public int Escola { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Curso")]
        public int Curso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Ano")]
        [RegularExpression("([1-9]+){1,1}", ErrorMessage = "Insira um número válido.")]
        public string Ano { get; set; }
        #endregion


        #region contacts
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido.")]
        [Display(Name = "Email Alternativo")]
        public string EmailAlternativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Contacto Pessoal")]
        public string ContactoPessoal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Contacto de Emergência")]
        public string ContactoEmergencia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([1-9]+){1,1}", ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Parentesco")]
        public int Parentesco { get; set; }
        #endregion


        #region docs
        [Display(Name = "Carta de Motivação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public IFormFile CartaMotivacao { get; set; }

        public int DocID { get; set; }
        #endregion


        #region destino
        [Required(ErrorMessage = "É necessário escolher pelo menos um curso.")]
        public string SelectedCursos { get; set; }
        #endregion

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
