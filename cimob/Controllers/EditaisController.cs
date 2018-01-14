using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            return View(new EditaisViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context),
                TipoMobilidadeList = GetTiposMobilidade()
            });
        }

        //Método para obter os tipos de mobilidade existentes na bd para mostrar no dropdown list da inserção dos editais
        private List<TipoMobilidade> GetTiposMobilidade()
        {
            var tiposMobilidade = from p in _context.TiposMobilidade where p.Estagio == 0 select p;
            return tiposMobilidade.ToList();
        }


        // POST: Edital
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertEdital(EditaisViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = _context.Editais.Add(new Edital
                {
                    Nome = model.Nome,
                    Caminho = model.CarregarEdital.ToString(),
                    TipoMobilidade = model.TipoMobilidadeList.ElementAt(0),
                    DataLimite = model.DataLimite
                });
            } 
            

            return View(model);
        }

    }
}