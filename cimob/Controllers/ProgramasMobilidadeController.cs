using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cimob.Controllers
{
    public class ProgramasMobilidadeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Método que retorna o modal dos programas de mobilidade
        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}