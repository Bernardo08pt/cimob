using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// ViewModel que corresponde à View Documentats (quaisquer campos, quer preenchiveis 
/// pelo utilizador ou não que possuem qualquer interação com a base de dados)
/// </summary>
namespace cimob.Models.EditaisViewModels
{
    public class ApplicationDocumentsViewModel
    {
        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; internal set; }

        /// <summary>
        /// Listagem dos documentos da candidatura
        /// </summary>
        public List<CandidaturaDocumentos> DocumentosList { get; internal set; }

        /// <summary>
        /// ID da candidatuara a ser visualizada
        /// </summary>
        public int CandidaturaID { get; set; }

        /// <summary>
        /// Novo documento a ser carregado para o servidor
        /// </summary>
        [Display(Name = "Novo documento")]
        public IFormFile Documento { get; set; }
    }
}
