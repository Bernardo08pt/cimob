using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using cimob.Models;
using cimob.Models.ApplicationViewModels;
using cimob.Services;
using cimob.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace cimob.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            ILogger<ApplicationController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        // GET: Application
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            return View(new ApplicationViewModel {
                AjudasDictionary = GetAjudas(new List<string>(new string[] { "Application" })),
                Escolas = GetEscolas(),
                Escola = GetEscolasIPS(),
                Curso = new List<IpsCurso>(),
                Paises = GetPaises(),
                Parentesco = GetParentesco(),
                DataNascimento = user.DataNascimento,
                Numero = user.Numero,
                Nome = user.Nome,
                Email = user.Email
            });
        }

        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Application/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Application/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Application/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Application/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Application/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Application/GetCursosByEscola/1
        [HttpGet]
        public ActionResult GetCursosByEscola(int id, IFormCollection collection)
        {
            return Json(_context.IpsCursos.Where(c => c.IpsEscolaID == id).ToList());
        }

        // GET: Application/FilterDestino?nome=ze&pais=alemanha
        [HttpGet]
        public ActionResult FilterDestino(int id, IFormCollection collection)
        {
            return Json(_context.Escolas.AsEnumerable().Where((e) => {
                var nome = HttpContext.Request.Query["nome"];
                var pais = HttpContext.Request.Query["pais"];
                
                if (nome != "" && pais == "")
                    return e.Nome.Contains(nome);


                if (nome == "" && pais != "")
                    return e.PaisID == pais;
                
                return e.Nome.Contains(nome) && e.PaisID == pais;
            }).ToList());
        }

        


        /** HELPER FUNCTIONS **/
        private IDictionary<string, Ajuda> GetAjudas(List<string> campos)
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

        private List<Escola> GetEscolas()
        {
            return _context.Escolas.Include(e => e.Cursos).ToList();
        }

        private List<IpsEscola> GetEscolasIPS()
        {
            return _context.IpsEscolas.ToList();
        }

        private List<IpsCurso> GetCursoIPS()
        {
            return _context.IpsCursos.ToList();
        }

        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        private List<Parentesco> GetParentesco()
        {
            return _context.Parentescos.ToList();
        }
    }
}