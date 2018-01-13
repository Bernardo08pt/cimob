using cimob.Data;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Extensions
{
    public class HelperFunctionsExtensions
    {
        public static IDictionary<string, Ajuda> GetAjudas(List<string> campos, ApplicationDbContext _context)
        {
            var ajudasContext = _context.Ajudas;
            var ajudas = from a in ajudasContext select a;
            ajudas = ajudas.Where(a => campos.Contains(a.Pagina));

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

        public static void AddErrors(IDictionary result, ModelStateDictionary modelState)
        {
            foreach (var error in result)
            {
                modelState.AddModelError(string.Empty, error.ToString());
            }
        }
    }
}
