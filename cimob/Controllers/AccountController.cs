using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cimob.Models;
using cimob.Models.AccountViewModels;
using cimob.Services;
using cimob.Data;
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

        /// <summary>
        /// Devolve a view da página de login
        /// </summary>
        /// <param name="returnUrl">Para onde redirecionar depois de login (default é null)</param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Tenta fazer login: verifica se os daods existem e se o email já foi confirmado
        /// Se conseguir rediciona para a home page (ou returnUrl), caso contrário 
        /// mostra a mesma view com os erros
        /// </summary>
        /// <param name="model">ViewModel relativo ao login</param>
        /// <param name="returnUrl">Possível url para onde redirecionar depois de login</param>
        /// <returns>View</returns>
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
                        ModelState.AddModelError(string.Empty, "O Email ainda não está validado, verifique a sua caixa de correio.");
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

        /// <summary>
        /// Retonar a view de Lockout
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// Retorna a view de registar
        /// </summary>
        /// <returns>View com o form de registar</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Registo" }), _context),
                DataNascimento = DateTime.Today
            });
        }

        /// <summary>
        /// Valida os campos inseridos. Se corretos, cria novo registo na bd e redireciona o utilizador para 
        /// a página de confirmação de registo. se não volta a mostrar o form com os erros de validação
        /// </summary>
        /// <param name="model">ViewModel respetivo aos campos de registo</param>
        /// <returns>View atual ou redirectToAction de confirmaão do registo</returns>
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
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Registo" }), _context);

            return View(model);
        }

        /// <summary>
        /// Retorna a view de confirmação do registo
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Termina a sessão do utilizador e redireciona-o para a página inicial
        /// </summary>
        /// <returns>RedirectToAction HomeController.Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Confirma o email do utilizador depois do registo com base no utilizador 
        /// e o código de confirmação. Se correto redireciona para a view ConfirmEmail
        /// caso contrário, a view Error
        /// </summary>
        /// <param name="userId">id do user cujo email está a ser confimrado</param>
        /// <param name="code">código de confirmação</param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Mostra o formulário de recuperação de palavra passe
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "RecuperarPassword" }), _context)
            });
        }

        /// <summary>
        /// Valida o formulário de recuperação de palavra passe. Se válido e utilziador existe, é enviado um email ao utilizador
        /// com um link para recuperar a palavra passe e depois redireciona para a view de confirmação de 
        /// recuperação de palavra passe. Caso contrário apenas redireciona para o mesmo ecrã (medida de segurança). Se não for
        /// válido mostra o formulário novamente mas com os erros de validação
        /// </summary>
        /// <param name="model">ViewModel correspondente ao formulário de recuperação de palavra passe </param>
        /// <returns>View</returns>
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
                await EmailSenderExtensions.RecuperarPassword(
                    _emailSender, 
                    model.Email, 
                    user.Nome, 
                    Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme)
                );
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            // Get page help
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "RecuperarPassword" }), _context);

            return View(model);
        }

        /// <summary>
        /// Mostra o ecrã de confirmação de recuperação de password
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Mostra o ecrã de recuperação de password se existir um código no link, caso contrário lança uma exceção
        /// </summary>
        /// <param name="code">código de recuperação</param>
        /// <returns>View ou exceção</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }

            return View(new ResetPasswordViewModel {
                Code = code,
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "AlterarPassword" }), _context)
            });
        }

        /// <summary>
        /// Valida o formulário de recuperação de palavra passe. Se incorreto volta a mostrar o formulário de 
        /// recuperação de palavra passe com os erros de validação. Caso contrário vai procurar o user com o email inserido.
        /// Se não encontrar, redireciona para a view ResetPasswordConfirmation por questões de segurança. Se encontrar, tenta atualizar
        /// fazer reset à password. Se conseguir, redireciona para a mesma view. Se não conseguir, mostra a view atual com os erros de validação
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Get page help
                model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "AlterarPassword" }), _context);

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

        /// <summary>
        /// Mostra o ecrã de confirmação de recuperação de password
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Mostra o ecrã de acesso negado
        /// </summary>
        /// <returns>View</returns>
        [HttpGet] 
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers
        /// <summary>
        /// função auxiliar que redireciona para determinado url ou página inicial caso não houver nenhum 
        /// depois de login
        /// </summary>
        /// <param name="returnUrl">página para onde redirecionar</param>
        /// <returns></returns>
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
