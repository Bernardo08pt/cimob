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

        public ActionResult Index()
        {
            if (HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User).User != null)
                return RedirectToAction("State", "Application");
            
            return View();
        }

        //Método que retorna o modal dos programas de mobilidade
        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}