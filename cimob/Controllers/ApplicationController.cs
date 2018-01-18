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
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

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
                Email = user.Email
            });
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
                    AnoLetivo = model.AnoLetivo,
                    ContactoPessoal = model.ContactoPessoal,
                    DataNascimento = model.DataNascimento,
                    EmailAlternativo = model.EmailAlternativo,
                    EmegerenciaParentescoID = model.Parentesco,
                    EmergenciaContacto = model.ContactoEmergencia,
                    TipoMobilidadeID = model.TipoMobilidade,
                    Entrevista = "",
                    EstadoCandidaturaID = 1,
                    Estagio = 1,
                    IpsCursoID = model.Curso,
                    Observacoes = "",
                    Pontuacao = 0,
                    RejeicaoRazao = "",
                    Rejeitada = -1,
                    Semestre = GetSemestre(),
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
                            DocumentoID = model.DocID
                        });

                    _context.SaveChanges();

                    _logger.LogInformation("User created a new application.");

                    var mobilidade = _context.TiposMobilidade.Where(p => p.TipoMobilidadeID == model.TipoMobilidade).Select(p => p.Descricao).FirstOrDefault();

                    var mensagem = "<p><span style='font-size: 18px;'>Caro(a) " + user.Nome + ",<strong> </strong></span></p>" +
                    "<p><span style='font-size: 18px;'>Gostaríamos de informar que a sua candidatura para " + mobilidade + " foi efetuada com sucesso! Iremos avaliar assim que possível e mantêmo-lo-emos informado.</span></p>" +
                    "<p><br></p>" +
                    "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                    "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                        "<span style = 'font-size: 12px;'> &nbsp;</span></p>";

                    await _emailSender.SendEmailAsync(model.Email, "Candidatura efetuada", mensagem);
                    await _emailSender.SendEmailAsync(model.EmailAlternativo, "Candidatura efetuada", mensagem);

                    return RedirectToAction(nameof(State));
                }
            }

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
        [Route("[controller]/Not-Found")]
        public IActionResult Not_Found()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/State")]
        public IActionResult State()
        {
            var tmp = HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User);

            if (tmp.User == null)
                return View(nameof(Not_Found));

            return View(new ApplicationStateViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                EstadosList = GetEstadosCandidatura(),
                Estado = _context.Candidaturas.Where(c => c.CandidaturaID == tmp.Candidatura).Select(c => c.EstadoCandidaturaID).FirstOrDefault()
            });
        }

        [HttpGet]
        [Route("[controller]/Documents")]
        public IActionResult Documents()
        {
            var tmp = HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User);

            if (tmp.User == null)
                return View(nameof(Not_Found));

            return View(new ApplicationDocumentsViewModel{
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                DocumentosList = GetDocumentosCandidatura(tmp.Candidatura),
            });
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
            var nome = Request.Query["nome"].Count == 0 ? "" : Request.Query["nome"][0].ToLower();
            var pais = Request.Query["pais"].Count == 0 ? "" : Request.Query["pais"][0];

            var res = _context.Escolas.Include(e => e.Cursos).Include(e => e.Pais).AsEnumerable().Where((e) =>
            {
                if (nome != "" && pais == "")
                    return e.Nome.ToLower().Contains(nome);

                if (nome == "" && pais != "")
                    return e.PaisID == int.Parse(pais);

                return e.Nome.ToLower().Contains(nome) && e.PaisID == int.Parse(pais);
            }).ToList();

            var tmp = new List<object>();

            res.ForEach(item => {
                item.Cursos.ToList().ForEach(c => {
                    tmp.Add(new {
                        escola = item.Nome,
                        pais = item.Pais.Descricao,
                        curso = c.Nome,
                        id = c.CursoID
                    });
                });
            });
            
            return Json(tmp);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var d = new Documento
                {
                    FicheiroCaminho = await FileHandling.Upload(file, "Candidaturas"),
                    FicheiroNome = file.FileName,
                    OrigemCimob = 0,
                    DataUpload = DateTime.Today

                };
                _context.Documentos.Add(d);
                _context.SaveChanges();

                return Json(new { status = "success", data = d });
            }
            catch (Exception ex)
            {
                return Json(
                    new {
                        status = "error",
                        data = HelperFunctionsExtensions.GetError((
                            ex is FileSizeException ? "FileTooBig" : 
                            ex is FormatException ? "InvalidFormat" : 
                            "InvalidFile"
                        ), _context)
                    }
                );
            }
        }

        // GET: Application/Download/1
        [HttpGet]
        public ActionResult Download (int id)
        {
            try
            {
                var tmp = _context.Documentos.
                Where(d => d.DocumentoID == id).
                Select(d => new {
                    caminho = d.FicheiroCaminho,
                    nome = d.FicheiroNome
                }).FirstOrDefault();

                return FileHandling.Download(tmp.caminho, tmp.nome);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/NoFile.cshtml");
            }
        }

        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }

        // GET: Application/View/1
        [HttpGet]
        public ActionResult View (int id)
        {   
            try
            {
                return FileHandling.View(_context.Documentos.Where(d => d.DocumentoID == id).Select(d => d.FicheiroCaminho).FirstOrDefault());
            }
            catch (Exception)
            {
                return View("~/Views/Shared/NoFile.cshtml");
            }
        }

        /** HELPER FUNCTIONS **/
        private List<Escola> GetEscolas()
        {
            return _context.Escolas.Include(e => e.Cursos).Where(e => e.Estado == 1).ToList();
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

        private List<EstadoCandidatura> GetEstadosCandidatura()
        {
            return _context.EstadosCandidatura.ToList();
        }

        private List<CandidaturaDocumentos> GetDocumentosCandidatura(int id)
        {
            return _context.CandidaturaDocumentos.Include(cd => cd.Documento).Where(cd => cd.CandidaturaID == id).ToList();
        }
    }
}