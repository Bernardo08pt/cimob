using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cimob.Models;
using Microsoft.AspNetCore.Authorization;

namespace cimob.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Verifica qual é o role do utilizador. Se for funcionário redireciona para a pagina de serviços cimob
        /// caso contrário, para a pagina de perfil
        /// </summary>
        /// <returns>Redirect to action</returns>
        public IActionResult Index()
        {
            if (User.IsInRole("Funcionario"))
                return RedirectToAction("Index", "ServicosCimob");
            
            else 
                return RedirectToAction("Profile", "Manage");
        }

        /// <summary>
        /// Mostra página de erro
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
