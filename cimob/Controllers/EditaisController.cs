using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.ApplicationViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cimob.Controllers
{
    public class EditaisController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public EditaisController(
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

        [HttpGet]
        public IActionResult Index()
        {
            //ESTA A DAR ERRO AQUI
            /*var alert = ViewBag.ShowEditalAlert;
            if(ViewBag.ShowEditalAlert != null)
            {
                alert = ViewBag.ShowEditalAlert;
            } else
            {
                ViewBag.ShowEditalAlert = false;
            }*/
            
            return View(new EditaisViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context),
                TipoMobilidadeList = GetTiposMobilidade(),
                Editais = GetEditais()
            });
        }

        // POST: Edital
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EditaisViewModel model)
        {

            if (ModelState.IsValid)
            {
                var optionSelected = from p in _context.TiposMobilidade where p.TipoMobilidadeID == model.ProgramaMobilidadeID select p;

                Edital e = new Edital
                {
                    Nome = model.Nome,
                    Caminho = "SW", //TODO: Por o caminho certo
                    TipoMobilidade = optionSelected.First(),
                    DataLimite = model.DataLimite,
                    Estado = 0
                };


                var result = from edital in _context.Editais where edital.Nome == e.Nome select edital;

                //Obter todos os utilizadores, convêm ser é os que são só candidatos
                var allUsers = from utilizador in _context.Users select utilizador;
                if (!result.Any())
                {
                    _context.Editais.Add(e);
                    _context.SaveChanges();
                    foreach (var u in allUsers)
                    {
                        // For more information on how to enable account confirmation and password reset please
                        // visit https://go.microsoft.com/fwlink/?LinkID=532713

                        await _emailSender.SendEmailAsync(u.Email, "Edital Publicado",
                        "<p><span style='font-size: 18px;'>Caro(a) " + u.Nome + ",<strong> </strong></span></p>" +
                        "<p><span style='font-size: 18px;'>Gostaríamos de informar que foi publicado o edital referente a " + e.TipoMobilidade.Descricao+ ".</span></p>" +
                        "<p><br></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                        "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                         "<span style = 'font-size: 12px;'> &nbsp;</span></p>");
                    }
                }
                else
                {
                   // ViewBag.ShowEditalAlert = true;
                    //mostrar um popup
                }
                
            }

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);
            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Editais = GetEditais();

             return View(model);
        }

        //Método para obter os tipos de mobilidade existentes na bd para mostrar no dropdown list da inserção dos editais
        private List<TipoMobilidade> GetTiposMobilidade()
        {
            var tiposMobilidade = from p in _context.TiposMobilidade where p.Estagio == 0 select p;
            return tiposMobilidade.ToList();
        }

        //Método para obter os editais
        [HttpGet]
        private List<Edital> GetEditais()
        {
            var tipoEdital = from edital in _context.Editais select edital;
            //se a data tiver expirado altera-se o valor do estado para 1
            foreach (var e in tipoEdital)
            {
                if(e.DataLimite < DateTime.Now)
                {
                    e.Estado = 1;
                }
            }
            if(tipoEdital != null)
            {

                return tipoEdital.ToList();
            }
            return new List<Edital>();
        }


    }
}