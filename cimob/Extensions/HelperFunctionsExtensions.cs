using cimob.Data;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace cimob.Extensions
{
    public class HelperFunctionsExtensions
    {
        public static IDictionary<string, Ajuda> GetAjudas(List<string> campos, ApplicationDbContext _context)
        {
            var ajudas = _context.Ajudas.Where(a => campos.Contains(a.Pagina));
            
            IDictionary<string, Ajuda> ajudasDictionary = new Dictionary<string, Ajuda>();

            foreach (Ajuda a in ajudas)
            {
                ajudasDictionary[a.Nome] = a;
            }

            return ajudasDictionary;
        }

        public static void AddErrors(IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }

        public static string GetError(string err, ApplicationDbContext _context)
        {
            return _context.Erros.Where(e => e.Nome == err).Select(e => e.Mensagem).FirstOrDefault();
        }
    }
}
