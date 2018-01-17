using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cimob.Models;
using Microsoft.AspNetCore.Authorization;

namespace cimob.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Funcionario"))
                return RedirectToAction("Index", "ServicosCimob");
            
            else 
                return RedirectToAction("Profile", "Manage");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
