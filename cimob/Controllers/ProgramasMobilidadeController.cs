using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cimob.Controllers
{
    public class ProgramasMobilidadeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProgramasMobilidadeController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Verifica se o utilizador já tem uma candidatura feita. Se tiver é redirecionado para 
        /// a página de estado da candaitura. Senão, devolve a página de escolha de programas de mobilidade
        /// </summary>
        /// <returns>View / redirectToAction</returns>
        public ActionResult Index()
        {
            if (HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User).User != null)
                return RedirectToAction("State", "Application");
            
            return View();
        }

        /// <summary>
        /// Retorna a modal do programa de mobilidade erasmus+ 
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}