
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using cimob.Models;
using cimob.Models.AccountViewModels;
using cimob.Services;
using cimob.Data;
using Microsoft.EntityFrameworkCore;
using cimob.Extensions;

namespace cimob.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
                    
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Get page help
            LoginViewModel model = new LoginViewModel
            {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas( new List<string>(new string[] { "Login" }), _context)
            };

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // Get page help
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Login" }), _context);

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // Require the user to have a confirmed email before they can log on.
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                                      "O Email ainda não está validado, verifique a sua caixa de correio.");
                        return View(model);
                    }
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Registo" }), _context),
                DataNascimento = DateTime.Today
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome,
                    Numero = Convert.ToInt32(model.Numero), DataNascimento = model.DataNascimento};

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //here we tie the new user to the role
                    await _userManager.AddToRoleAsync(user, "Candidato");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, model.Nome, callbackUrl);
                    
                   // await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction(nameof(RegisterConfirmation));
                }

                HelperFunctionsExtensions.AddErrors(result, ModelState);
            }

            // If we got this far, something failed, redisplay form
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Login" }), _context);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            // Get page help
            ForgotPasswordViewModel model = new ForgotPasswordViewModel
            {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "RecuperarPassword" }), _context)
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Recuperação de Password",
                "<p><span style='font-size: 18px;'>Caro(a) " + user.Nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>Recebemos um pedido de recuperação da palavra passe associada a este e-mail. Se não fez este pedido, ignore este e-mail (a sua conta continua segura).</span></p>" +
                "<p><span style='font-size: 18px;'>Caso contrário clique <a href='" + callbackUrl + "'>aqui</a> para começar o processo de redefinição de palavra passe.&nbsp;</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                 "<span style = 'font-size: 12px;'> &nbsp;</span></p>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            // Get page help
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "RecuperarPassword" }), _context);


            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };

            // Get page help
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "AlterarPassword" }), _context);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            // Get page help
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "AlterarPassword" }), _context);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            HelperFunctionsExtensions.AddErrors(result, ModelState);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet] 
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion
    }
}
