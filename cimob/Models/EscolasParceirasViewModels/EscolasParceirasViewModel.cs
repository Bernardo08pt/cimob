using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.EscolasParceirasViewModels
{
    public class EscolasParceirasViewModel
    {
        #region aux
        public List<Pais> PaisesList { get; set; }
        public List<TipoMobilidade> MobilidadeList { get; set; }
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
        #endregion 

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "País")]
        public int Pais { get; set; }

        [Display(Name = "Estado")]
        public int Estado { get; set; }

        public List<Curso> Cursos { get; set; }

        [Required(ErrorMessage = "É obrigatório escolher um programa.")]
        public int Mobilidade { get; set; }

        [Display(Name = "Nome")]
        public string NomeCurso { get; set; }

        [Display(Name = "Vagas")]
        public int Vagas { get; set; }

        [Display(Name = "Cursos")]
        public List<string> CursosNovos { get; set; }
    }
}
