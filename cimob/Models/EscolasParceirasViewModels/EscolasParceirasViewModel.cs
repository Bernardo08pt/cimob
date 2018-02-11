using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cimob.Models.EscolasParceirasViewModels
{
    /// <summary>
    /// ViewModel que corresponde à View resposável pela Criação / edição de escolas parceiras 
    /// (quaisquer campos, quer preenchiveis pelo utilizador ou não que possuem qualquer interação com a base de dados)
    /// </summary>
    public class EscolasParceirasViewModel
    {
        #region aux
        /// <summary>
        /// Listagem dos paises exitentes para a combobx
        /// </summary>
        public List<Pais> PaisesList { get; set; }

        /// <summary>
        /// Listagem dos tipos de mobilidade existentes para preencher a combobox
        /// </summary>
        public List<TipoMobilidade> MobilidadeList { get; set; }
        
        /// <summary>
        /// Ajudas ao utilizador da página e campos
        /// </summary>
        public IDictionary<string, Ajuda> AjudasDictionary { get; set; }
        #endregion 

        /// <summary>
        /// Nome da escola parceira
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Email da escola parceira
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// País da escola parceira
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "País")]
        public int Pais { get; set; }

        /// <summary>
        /// Estado atual dos acordos bilaterais com a escola parceira
        /// (indica se o candidato pode escolher esta escola como um possível destino ou não)
        /// </summary>
        [Display(Name = "Estado")]
        public int Estado { get; set; }

        /// <summary>
        /// Listagem de cursos que esta escola possui
        /// </summary>
        public List<Curso> Cursos { get; set; }

        /// <summary>
        /// Tipo de Mobilidade ao qual esta escola pertence
        /// </summary>
        [Required(ErrorMessage = "É obrigatório escolher um programa.")]
        public int Mobilidade { get; set; }

        /// <summary>
        /// Nome do novo curso a adicionar à escola
        /// </summary>
        [Display(Name = "Nome")]
        public string NomeCurso { get; set; }

        /// <summary>
        /// Número de vagas que o novo curso tem
        /// </summary>
        [Display(Name = "Vagas")]
        public int Vagas { get; set; }

        /// <summary>
        /// Listagem dos cursos novos adicionados em JSON
        /// </summary>
        [Display(Name = "Cursos")]
        public List<string> CursosNovos { get; set; }
    }
}
