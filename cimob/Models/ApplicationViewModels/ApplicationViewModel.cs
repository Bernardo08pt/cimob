using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.ApplicationViewModels
{
    public class ApplicationViewModel
    {
        #region general 
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Insira um número válido.")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [BirthDateValidation(ErrorMessage = "A idade tem de ser entre 17 a 100 anos.")]
        [DataType(DataType.Date, ErrorMessage = "A data é inválida.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Escola")]
        public List<IpsEscola> Escola { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Curso")]
        public List<IpsCurso> Curso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Ano")]
        public int Ano { get; set; }
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
        [Display(Name = "Parentesco")]
        public string Parentesco { get; set; }
        #endregion


        #region docs
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Carta de Motivação")]
        public IFormFile CartaMotivacao { get; set; }
        #endregion


        #region destino
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public List<string> Escolhas { get; set; }
        
        public List<Escola> Escolas { get; set; }

        public int SearchPais { get; set; }
        public string SearchName { get; set; }
        public List<Pais> Paises { get; set; }
        #endregion

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
