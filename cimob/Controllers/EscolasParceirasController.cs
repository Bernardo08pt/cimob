using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.EscolasParceirasViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace cimob.Controllers
{
    [Authorize(Roles = "Funcionario")]
    [Route("Escolas")]
    public class EscolasParceirasController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public EscolasParceirasController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<ApplicationController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Escolas()
        {
            var ajudas = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context);
            
            ViewBag.EditingCurso = false;

            return View(new MergeViewModel {
                EscolasViewModel = new EscolasParceirasViewModel {
                    PaisesList = GetPaises(),
                    MobilidadeList = GetMobilidade(),
                    AjudasDictionary = ajudas
                },
                CursosViewModel = new CursosViewModel {
                    AjudasDictionary = ajudas
                }
            });
        }

        // GET: /Escolas/Create
        [HttpGet]
        [Route("/Create")]
        public IActionResult Create()
        {
            return View(new EscolasParceirasViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context),
                MobilidadeList = GetMobilidade(),
                PaisesList = GetPaises()
            });
        }

        [HttpPost]
        [Route("/Create")]
        public IActionResult Create(EscolasParceirasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tmp = new List<Curso>();

                model.Cursos.Split(",").ToList().ForEach(item => {
                    tmp.Add(_context.Cursos.Where(c => c.CursoID == int.Parse(item)).FirstOrDefault());
                });

                var e = new Escola
                {
                    Email = model.Email,
                    Estado = 1,
                    Nome = model.Nome,
                    PaisID = model.Pais,
                    Cursos = tmp
                };

                var result = _context.Escolas.Add(e);

                if (result.State.ToString() == "Added")
                {
                    _context.SaveChanges();

                    _context.Cursos.ToList().ForEach(item => {
                        if (tmp.Contains(item))
                            item.EscolaID = e.EscolaID;
                    });

                    _context.SaveChanges();

                    _logger.LogInformation("User created a new escola parceira.");

                    return RedirectToAction(nameof(Escolas));
                }
            }

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context);
            model.PaisesList = GetPaises();
            model.MobilidadeList = GetMobilidade();

            ViewBag.EditingCurso = false;
            return View(model);
        }

        [HttpPost]
        [Route("Escolas/Create/Curso")]
        public IActionResult CreateCurso(CursosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var c = new Curso
                {
                    Nome = model.Nome,
                    Vagas = model.Vagas,
                };

                var result = _context.Cursos.Add(c);

                if (result.State.ToString() == "Added")
                {
                    _context.SaveChanges();

                    _logger.LogInformation("User created a new escola parceira.");

                    ViewBag.EditingCurso = false;
                    return RedirectToAction(nameof(Create));
                }
            }

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context);

            ViewBag.EditingCurso = true;

            return View(model);
        }


        /** HELPER FUNCTIONS **/
        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        private List<TipoMobilidade> GetMobilidade()
        {
            return _context.TiposMobilidade.ToList();
        }
    }
}