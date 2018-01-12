using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimob.Data;
using cimob.Models;
using cimob.Models.ApplicationViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cimob.Controllers
{
    public class VisualizarCandidaturaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public VisualizarCandidaturaController(
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
            return View();
        }

        //Método para obter as ajudas da BD
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
    }
}