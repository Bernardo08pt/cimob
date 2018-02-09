using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cimob.Models;
using cimob.Models.ManageViewModels;
using cimob.Services;
using cimob.Extensions;
using cimob.Data;

namespace cimob.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        
        /// <summary>
        /// Verifica se o utilizador já submeteu uma candidatura (pois isto muda os botões que são mostrados)
        /// Retorna a view do perfil do utilizador
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public IActionResult Profile()
        {
            //permite saber se o candidato ja submeteu uma candidatura
            ViewBag.CandidaturaSubmetida = (HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User).User != null);

            //Acedemos a flag passada no EditProfile
            var alert = TempData["ShowAlert"];
            
            if (alert != null) //Caso nao seja null, passo a variavel por View Bag e mostro o alert na página Área Pessoal
            {
                ViewBag.ShowAlert = alert;
            }
            else //Caso tenha clicado no botão voltar, envia-se a false para não dar um erro
            {
                ViewBag.ShowAlert = false;
            }

            return View();
        }
        
        /// <summary>
        /// Mostra o ecrã de edição de perfil preenchido com a informação do user
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new EditProfileViewModel {
                Numero = user.Numero,
                Nome = user.Nome,
                Email = user.Email,
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EditarPerfil" }), _context)
            });
        }

        /// <summary>
        /// Verifica se os dados inseridos estão válidos (preenchidos corretamente, palavra pass antiga 
        /// correta, novas palavras passe coincidem). Se sim, atualiza os campos na BD, caso contrário
        /// mostra o respetivo erro
        /// </summary>
        /// <param name="model">ViewModel relativo ao perfil do utilizador</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            var err = false;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
                err = true;
            
            if (!err && !await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                err = true;
                ModelState.AddModelError("OldPassword", "Password atual incorreta!");
            }
            
            // Se a nova password ou o confirm password não forem vazios 
            if (!err)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    err = true;
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            if (err)
            {
                model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EditarPerfil" }), _context);
                model.Numero = user.Numero;
                model.Nome = user.Nome;
                model.Email = user.Email;

                return View(model);
            }

            //Passa-se uma flag para mostrar o alert para a action Profile
            TempData["ShowAlert"] = true;

            _context.SaveChanges();

            //Depois de submeter a alteração da password volta para a página Área Pessoal
            return RedirectToAction(nameof(Profile));
        }
    }
}