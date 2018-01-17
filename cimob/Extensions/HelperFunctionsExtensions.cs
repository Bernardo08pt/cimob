using cimob.Data;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace cimob.Extensions
{
    public class HelperFunctionsExtensions
    {
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

        internal static void AddErrors(IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }

        internal static string GetError(string err, ApplicationDbContext _context)
        {
            return _context.Erros.Where(e => e.Nome == err).Select(e => e.Mensagem).FirstOrDefault();
        }


        internal struct UserCandidatura {
            public string User;
            public int Candidatura;
        }

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
