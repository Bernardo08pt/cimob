using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.EditaisViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View dos Editais (quaisquer campos, quer preenchiveis 
    /// pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class EditaisViewModel
    {
        #region inserir editais
        /// <summary>
        /// Ficheiro a ser carregado
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Carregar edital")]
        public IFormFile CarregarEdital { get; set; }

        /// <summary>
        /// Nome do ficheiro
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome do edital")]
        public string Nome { get; set; }

        /// <summary>
        /// Data limite do edital (até quando este está ativo)
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data limite")]
        [DataType(DataType.Date, ErrorMessage = "A data é inválida.")]
        public DateTime DataLimite { get; set; }
        
        /// <summary>
        /// Tipo de mobilidade ao qual o edital pertence
        /// </summary>
        // Passou a ser int para depois ir buscar a dropdownlist o objeto com o esse ID
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Programa de mobilidade")]
        public int ProgramaMobilidadeID { get; set; }

        /// <summary>
        /// Listagem dos tipos de mobilidade exitentes
        /// </summary>
        public List<TipoMobilidade> TipoMobilidadeList { get; set; }
        #endregion

        #region mostrar editais
        /// <summary>
        /// Listagem com os editais existenes
        /// </summary>
        public List<Edital> Editais;
        #endregion

        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}
