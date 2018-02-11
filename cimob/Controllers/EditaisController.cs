using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.EditaisViewModels;
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

        /// <summary>
        /// Devolve a view Index correspondente aos editais
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Valida os campos inseridos. Se estiverem válidos carrega o ficheiro para o servidor e 
        /// cria um novo edital na BD. Caso contrário devolve a view com os erros de validação
        /// </summary>
        /// <param name="model">ViewModel correspondente aos editais</param>
        /// <returns>View com erros de validação ou mensagem de sucesso</returns>
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
                    await EmailSenderExtensions.EditalPublicado(_emailSender, u.Nome, u.Email, mobilidade);
                }

                //Serve para mostrar o alert de sucesso/insucesso
                ViewBag.IsSucceded = true;
            }
            
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);
            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Editais = GetEditais();

            return View(model);
        }

        
        /// <summary>
        /// Devovle a view com a listagem dos editais existentes
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public IActionResult VisualizarEditais()
        {
            return View(new EditaisViewModel
            {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context),
                TipoMobilidadeList = GetTiposMobilidade(),
                Editais = GetEditais()
            });
        }

        /// <summary>
        /// Procura o ficheiro com o ID recebido. Se encontrado, descarrega-o, casa contrário devolve NoFile view
        /// </summary>
        /// <param name="id">id do ficheiro que estamos à procura</param>
        /// <returns>o ficheiro a descarregar ou view</returns>
        // GET: Editais/VisualizarEditais/Download/{id}
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
                return RedirectToAction(nameof(NoFile));
            }
        }

        /// <summary>
        /// Obtém os tipos de mobilidade existentes na bd para mostrar no dropdown list da inserção dos editais
        /// </summary>
        /// <returns>Lista de TipoMobilidade</returns>
        private List<TipoMobilidade> GetTiposMobilidade()
        {
            return _context.TiposMobilidade.Where(p => p.Estagio == 0).ToList();
        }

        /// <summary>
        /// View do NoFile
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }

        /// <summary>
        /// obtém os editais
        /// </summary>
        /// <returns>Lista de Edital</returns>
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

        /// <summary>
        /// Adiciona erros ao model para retornar com a view
        /// </summary>
        /// <param name="model">model ao qual vão ser adicionados os erros</param>
        /// <param name="error">erro a adicionar</param>
        /// <returns>Model com os erros</returns>
        private EditaisViewModel SetupError(EditaisViewModel model, string error)
        {
            ViewBag.Message = HelperFunctionsExtensions.GetError(error, _context);
            ViewBag.IsSucceded = false;

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);
            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Editais = GetEditais();
            
            return model;
        }
        
        /// <summary>
        /// Devolve a view de edição de editais com a informação do edital respetivo ao id recebido
        /// </summary>
        /// <param name="id">id do edital que queremos editar</param>
        /// <returns>View</returns>
        [HttpGet]
        [Authorize(Roles = "Funcionario")]
        [Route("[controller]/{id}/Editar")]
        public IActionResult Editar(int id)
        {
            try
            {
                var editalAEditar = _context.Editais.Where(e => e.EditalID == id).FirstOrDefault();

                return View(new EditaisViewModel {
                    TipoMobilidadeList = GetTiposMobilidade(),
                    DataLimite = editalAEditar.DataLimite,
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

        /// <summary>
        /// Valida se os campos obrigatório estão corretos. Se estiverem altera o edital e redireciona para a listagem 
        /// Caso contrário devolve a view com os erros de validação
        /// </summary>
        /// <param name="model">ViewModel correspondente aos editais</param>
        /// <param name="id">id do edital a editar</param>
        /// <returns>View</returns>
        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        [Route("[controller]/{id}/Editar")]
        public async Task<IActionResult> Editar(EditaisViewModel model, int id)
        {
            if (model.DataLimite.CompareTo(DateTime.Now) <= 0)
                ModelState.AddModelError("DataLimite", "Data inválida. Tem que ser superior à atual");

            var result = _context.Editais.Where(e => e.EditalID == id).FirstOrDefault();
            
            try
            {
                using (var db = _context)
                {
                    if (result != null)
                    {
                        FileHandling.Remove(result.Caminho);

                        result.Caminho = await FileHandling.Upload(model.CarregarEdital, "Editais");
                        result.NomeFicheiro = model.CarregarEdital.FileName;
                        result.DataLimite = model.DataLimite;

                        db.SaveChanges();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CarregarEdital", 
                    HelperFunctionsExtensions.GetError((
                        ex is FileSizeException ? "FileTooBig" :
                        ex is FormatException ? "InvalidFormat" :
                        "InvalidFile"
                    ), _context)
                );
            }

            model.TipoMobilidadeList = GetTiposMobilidade();
            model.Nome = result.Nome;
            model.ProgramaMobilidadeID = result.TipoMobilidadeID;
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Editais" }), _context);

            return View(model);
        }
    }
}