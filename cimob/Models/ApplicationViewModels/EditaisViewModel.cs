﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Models.ApplicationViewModels
{
    public class EditaisViewModel
    {
        #region inserir editais
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Carregar edital:")]
        public IFormFile CarregarEdital { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data limite:")]
        [DataType(DataType.Date, ErrorMessage = "A data é inválida.")]
        public DateTime DataLimite { get; set; }
        #endregion

        #region mostrar editais
        public List<Edital> Editais;
        public SelectList ProgramasMobilidade;
        public string ProgramaMobilidade;
        #endregion

        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
    }
}