using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.ApplicationViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cimob.Controllers
{
    [Authorize]
    public class EditaisController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public EditaisController(
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
        public IActionResult Index()
        {
            return View(new EditaisViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context),
                TipoMobilidadeList = GetTiposMobilidade(),
                Editais = GetEditais(),
                DataLimite = DateTime.Now
            });
        }

        // POST: Edital
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Index(EditaisViewModel model)
        {
            if (model.DataLimite.CompareTo(DateTime.Now) <= 0)
                ModelState.AddModelError("DataLimite", "Data inválida. Tem que ser superior à atual");

            if (ModelState.IsValid)
            {
                try
                {
                    //Obter um edital que tenha o mesmo nome que o inserido no model
                    var result = _context.Editais.Where(ed => ed.Nome == model.Nome && ed.TipoMobilidadeID == model.ProgramaMobilidadeID).Count();

                    //Serve para mostrar o alert de sucesso/insucesso
                    if (result > 0)
                        return View(SetupError(model, "EditalExists"));

                    //Criar o edital com as informações que o utilizador inseriu na página
                    var e = new Edital
                    {
                        Nome = model.Nome,
                        Caminho = await FileHandling.Upload(model.CarregarEdital, "Editais"),
                        NomeFicheiro = model.CarregarEdital.FileName,
                        TipoMobilidadeID = model.ProgramaMobilidadeID,
                        DataLimite = model.DataLimite,
                        Estado = 0
                    };

                    _context.Editais.Add(e);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                { 
                    return View(SetupError(model, 
                        (ex is FileSizeException ? "FileTooBig" :
                            ex is FormatException ? "InvalidFormat" :
                            "InvalidFile")
                        )
                    );
                }
                
                //Obter todos os utilizadores, convêm ser é os que são só candidatos
                var allUsers = from utilizador in _context.Users select utilizador;
                var mobilidade = _context.TiposMobilidade.Where(p => p.TipoMobilidadeID == model.ProgramaMobilidadeID).Select(p => p.Descricao).FirstOrDefault();

                //email a notificar todos os utilizadores que um edital foi publicado
                foreach (var u in allUsers)
                {
                    //Envio do email
                    await _emailSender.SendEmailAsync(u.Email, "Edital Publicado",
                    "<p><span style='font-size: 18px;'>Caro(a) " + u.Nome + ",<strong> </strong></span></p>" +
                    "<p><span style='font-size: 18px;'>Gostaríamos de informar que foi publicado o edital referente a " + mobilidade + ".</span></p>" +
                    "<p><br></p>" +
                    "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                    "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                        "<span style = 'font-size: 12px;'> &nbsp;</span></p>");
                }

                //Serve para mostrar o alert de sucesso/insucesso
                ViewBag.IsSucceded = true;
            }
            
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);
            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Editais = GetEditais();

            return View(model);
        }


        [HttpGet]
        public IActionResult VisualizarEditais()
        {
            return View(new EditaisViewModel
            {
                TipoMobilidadeList = GetTiposMobilidade(),
                Editais = GetEditais()
            });
        }

        // GET: Editais/VisualizarEditais/Download/id
        [HttpGet]
        [Route("[controller]/VisualizarEditais/Download/{id}")]
        public ActionResult Download(int id)
        {
            try
            {
                var tmp = _context.Editais.
                    Where(d => d.EditalID == id).
                    Select(d => new {
                        caminho = d.Caminho,
                        nome = d.NomeFicheiro
                    }).FirstOrDefault();

                return FileHandling.Download(tmp.caminho, tmp.nome);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/NoFile.cshtml");
            }
        }

        //Método para obter os tipos de mobilidade existentes na bd para mostrar no dropdown list da inserção dos editais
        private List<TipoMobilidade> GetTiposMobilidade()
        {
            return _context.TiposMobilidade.Where(p => p.Estagio == 0).ToList();
        }

        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }

        //Método para obter os editais
        private List<Edital> GetEditais()
        {
            var tipoEdital = from edital in _context.Editais select edital;

            //Se não houver editais na BD envia-se uma lista vazia para não dar erro
            if (tipoEdital == null)
                return new List<Edital>();

            //se a data tiver expirado altera-se o valor do estado para 1
            foreach (var editalAux in tipoEdital)
            {
                if (editalAux.DataLimite < DateTime.Now)
                {
                    editalAux.Estado = 1;
                }
            }
            
            return tipoEdital.ToList();
        }

        private EditaisViewModel SetupError(EditaisViewModel model, string error)
        {
            ViewBag.Message = HelperFunctionsExtensions.GetError(error, _context);
            ViewBag.IsSucceded = false;

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);
            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Editais = GetEditais();
            
            return model;
        }
        
        //TESTE DE EDITAIS
        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                var editalAEditar = _context.Editais.Where(e => e.EditalID == id).FirstOrDefault();

                return View(new EditaisViewModel
                {
                    TipoMobilidadeList = GetTiposMobilidade(),
                    AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context),
                    Nome = editalAEditar.Nome,
                    ProgramaMobilidadeID = editalAEditar.TipoMobilidadeID,
                });
            }
            catch (Exception)
            {
                return View(nameof(Index));
            }

        }


        [HttpPost]
        public async Task<IActionResult> Editar(EditaisViewModel model, int id)
        {
            if (model.DataLimite.CompareTo(DateTime.Now) <= 0)
                ModelState.AddModelError("DataLimite", "Data inválida. Tem que ser superior à atual");

            if (ModelState.IsValid)
            {
                var edital = _context.Editais.Where(e => e.EditalID == 1).First();
                edital.Caminho = await FileHandling.Upload(model.CarregarEdital, "Editais");
                edital.NomeFicheiro = model.CarregarEdital.FileName;
                edital.DataLimite = model.DataLimite;
                
            }

            model.TipoMobilidadeList = GetTiposMobilidade();
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);

            return View(model);
        }






    }
}