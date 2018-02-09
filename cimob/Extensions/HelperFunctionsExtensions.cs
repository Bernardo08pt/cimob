using cimob.Data;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace cimob.Extensions
{
    /// <summary>
    /// Classe com funções auxiliares que são usadas em vários ficheiros
    /// </summary>
    public class HelperFunctionsExtensions
    {
        /// <summary>
        /// Função que vai à base de dados buscar as ajudas para a página em questão
        /// </summary>
        /// <param name="campos"></param>
        /// <param name="_context">ligação entre a aplicação e a bd</param>
        /// <returns>Dictionary com as ajudas</returns>
        internal static IDictionary<string, Ajuda> GetAjudas(List<string> campos, ApplicationDbContext _context)
        {
            var ajudas = _context.Ajudas.Where(a => campos.Contains(a.Pagina));

            IDictionary<string, Ajuda> ajudasDictionary = new Dictionary<string, Ajuda>();

            foreach (Ajuda a in ajudas)
            {
                ajudasDictionary[a.Nome] = a;
            }

            return ajudasDictionary;
        }

        /// <summary>
        /// Adiciona erros de validaçõ ao modelState para devolver com a view
        /// </summary>
        /// <param name="result">erros de validação</param>
        /// <param name="modelState">modelState atual que vamos devolver</param>
        internal static void AddErrors(IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// Obtém um erro da tabela de errors cujo titulo é igual ao err recebido
        /// </summary>
        /// <param name="err">err que estamos à procura</param>
        /// <param name="_context">ligação entre a aplicação e BD</param>
        /// <returns></returns>
        internal static string GetError(string err, ApplicationDbContext _context)
        {
            return _context.Erros.Where(e => e.Nome == err).Select(e => e.Mensagem).FirstOrDefault();
        }

        /// <summary>
        /// Estrutura temporária que guarda o id do user e da candidatura
        /// </summary>
        internal struct UserCandidatura {
            public string User;
            public int Candidatura;
        }

        /// <summary>
        /// Verifica se o utilizador logado tem alguma candidatura
        /// </summary>
        /// <param name="context">ligação entre a aplicação e a BD</param>
        /// <param name="userManager">UserManager para poder ir buscar o user</param>
        /// <param name="user">user atual</param>
        /// <returns>um novo objeto UserCandidatura</returns>
        internal static UserCandidatura GetUserCandidatura(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var id = userManager.GetUserId(user);

            return context.Candidaturas.
                Where(c => c.UtilizadorID == id).
                Select(c => new UserCandidatura {
                        User = id,
                        Candidatura = c.CandidaturaID
                    }).
                FirstOrDefault();
        }
    }
}
