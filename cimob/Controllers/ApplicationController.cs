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
using cimob.Extensions;

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
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Applications" }), _context),
                EscolasList = GetEscolas(),
                EscolaList = GetEscolasIPS(),
                CursoList = new List<IpsCurso>(),
                PaisesList = GetPaises(),
                ParentescoList = GetParentesco(),
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

        // POST: Application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Application(ApplicationViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                var result = _context.Candidaturas.AddAsync(new Candidatura
                {
                    AnoLetivo = model.Ano,
                    ContactoPessoal = model.ContactoPessoal,
                    DataNascimento = model.DataNascimento,
                    EmailAlternativo = model.EmailAlternativo,
                    EmegerenciaParentescoID = model.Parentesco,
                    EmergenciaContacto = model.ContactoEmergencia,
                    Entrevista = "",
                    EstadoCandidaturaID = 1,
                    Estagio = 1,
                    IpsCursoID = model.Curso,
                    Observacoes = "",
                    Pontuacao = 0,
                    RejeicaoRazao = "",
                    Rejeitada = -1,
                    Semestre = 1,
                    TipoMobilidadeID = model.TipoMobilidade,
                    UtilizadorID = user.Id
                });

                if (result.IsCompletedSuccessfully)
                {
                    _logger.LogInformation("User created a new application.");

                    return RedirectToAction(nameof(ApplicationConfirmation));
                }

                HelperFunctionsExtensions.AddErrors(result.Exception.Data, ModelState);
            }

            // If we got this far, something failed, redisplay form
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context);
            model.EscolasList = GetEscolas();
            model.EscolaList = GetEscolasIPS();
            model.CursoList = new List<IpsCurso>();
            model.PaisesList = GetPaises();
            model.ParentescoList = GetParentesco();
            model.DataNascimento = user.DataNascimento;
            model.Numero = user.Numero;
            model.Nome = user.Nome;
            model.Email = user.Email;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ApplicationConfirmation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateSelectedCurso(ApplicationViewModel model, [FromBody] int value, [FromBody] bool add)
        {
            if (add)
                model.SelectedCursos.Add(value);
            else
                model.SelectedCursos.Remove(value);

            return StatusCode(200);
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

        [HttpPost]
        public ActionResult UpdateCurso (ApplicationViewModel model, [FromBody] int value)
        {
            model.Curso = value;

            return StatusCode(200);
        }

        [HttpPost]
        public ActionResult UpdateEscola(ApplicationViewModel model, [FromBody] int value)
        {
            model.Escola = value;

            return StatusCode(200);
        }

        [HttpPost]
        public ActionResult UpdateParentesco(ApplicationViewModel model, [FromBody] int value)
        {
            model.Parentesco = value;

            return StatusCode(200);
        }


        /** HELPER FUNCTIONS **/
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

        private List<CandidaturaCursos> GetCursosCandidatura(ApplicationViewModel model)
        {
            var tmp = new List<CandidaturaCursos>();

            model.SelectedCursos.ForEach(c => tmp.Add(new CandidaturaCursos { CursoID = c }));

            return tmp;
        }
    }
}