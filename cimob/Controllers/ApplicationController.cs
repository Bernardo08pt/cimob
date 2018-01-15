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
using System.IO;

namespace cimob.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private List<int> selectedCurso;

        public ApplicationController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<ApplicationController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context; 
            selectedCurso = new List<int>();
        }

        // GET: Application
        [Route("[controller]")]
        public async Task<ActionResult> Application()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new ApplicationViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                EscolasList = GetEscolas(),
                EscolaList = GetEscolasIPS(),
                CursoList = new List<IpsCurso>(),
                PaisesList = GetPaises(),
                ParentescoList = GetParentesco(),
                DataNascimento = user.DataNascimento,
                Numero = user.Numero,
                Nome = user.Nome,
                Email = user.Email,
                SelectedCursos = "",
                Escola = 0,
                Curso = 0,
                Parentesco = 0
            });
        }

        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]")]
        public async Task<IActionResult> Application(ApplicationViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                var c = new Candidatura
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
                    Semestre = GetSemestre(),
                    TipoMobilidadeID = 1,
                    UtilizadorID = user.Id,
                    Documentos = null
                };

                var result = await _context.Candidaturas.AddAsync(c);
                
                if (result.State.ToString() == "Added")
                {
                    _context.SaveChanges();

                    var tmp = new List<CandidaturaCursos>();

                    model.SelectedCursos.Split(",").ToList().ForEach(item => {
                        tmp.Add(new CandidaturaCursos { CandidaturaID = c.CandidaturaID, CursoID = int.Parse(item) });
                    });
                    
                    _context.CandidaturaCursos.AddRange(tmp);

                    _context.SaveChanges();
                    
                    _context.CandidaturaDocumentos.Add(
                        new CandidaturaDocumentos {
                            CandidaturaID = c.CandidaturaID,
                            CandidaturaDocumentosID = model.DocID
                        });

                    _logger.LogInformation("User created a new application.");

                    return RedirectToAction(nameof(ApplicationConfirmation));
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ApplicationConfirmation()
        {
            return View();
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
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            var d = new Documento {
                FicheiroCaminho = await FileHandling.Upload(file),
                FicheiroNome = file.FileName,
                OrigemCimob = 0,
                DataUpload = DateTime.Today
            };

            _context.Documentos.Add(d);
            _context.SaveChanges();

            return Json(d);
        }

        // GET: Application/Download/1
        // TODO: Passar isto para o FileHandling
        [HttpGet]
        public FileResult Download (int id)
        {
            var tmp = _context.Documentos.
                Where(d => d.DocumentoID == id).
                Select(d => new {
                    caminho = d.FicheiroCaminho,
                    nome = d.FicheiroNome
                }).FirstOrDefault();
            
            return File(System.IO.File.ReadAllBytes(tmp.caminho), "application/x-msdownload", tmp.nome);
        }

        // GET: Application/View/1
        // TODO: Passar isto para o FileHandling
        [HttpGet]
        public ActionResult View (int id)
        {   
            return new FileStreamResult(
                new FileStream(
                    _context.Documentos.Where(d => d.DocumentoID == id).Select(d => d.FicheiroCaminho).FirstOrDefault(), 
                    FileMode.Open, 
                    FileAccess.Read
                ), 
                "application/pdf"
            );
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
            return _context.IpsCursos.OrderBy(c => c.Nome).ToList();
        }

        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        private List<Parentesco> GetParentesco()
        {
            return _context.Parentescos.ToList();
        }

        private short GetSemestre()
        {
            var mes = DateTime.Today.Month;
            return (short)((mes >= 8 && mes <= 2) ? 1 : 2);
        }
    }
}