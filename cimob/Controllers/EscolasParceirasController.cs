using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.EscolasParceirasViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cimob.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class EscolasParceirasController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public EscolasParceirasController(
            UserManager<ApplicationUser> userManager,
            ILogger<ApplicationController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Devolve a View das listagem de escolas parceiras
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [Route("/Escolas")]
        public IActionResult Index() {
            return View(new EscolasListViewModel {
                PaisesList = GetPaises(),
                EscolasList = GetEscolas(),
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context)
            });
        }

        /// <summary>
        /// Devolve a view relativa à criação de novas escolas
        /// </summary>
        /// <returns>view</returns>
        // GET: /Escolas/Create
        [HttpGet]
        [Route("/Escolas/Create")]
        public IActionResult Escolas()
        {
            return View(new EscolasParceirasViewModel {
                PaisesList = GetPaises(),
                MobilidadeList = GetMobilidade(),
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context),
                Cursos = new List<Curso>()
            });
        }

        /// <summary>
        /// Valida se os campos inseridos estão validos. Se sim cria uma nova escola na BD e redireciona para 
        /// a listagem de escolas. Caso contrário mostra a novamente mas com os erros 
        /// </summary>
        /// <param name="model">ViewModel relativo à criação / edição de escolas parceiras</param>
        /// <returns>RedirectToAction da view index ou view atual</returns>
        [HttpPost]
        [Route("/Escolas/Create")]
        public IActionResult Escolas(EscolasParceirasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tmp = (model.Cursos == null ? new List<Curso>() : new List<Curso>(model.Cursos));

                model.CursosNovos?.ForEach(item => {
                    var json = JsonConvert.DeserializeObject<Curso>(item);
                    var c = new Curso { Nome = json.Nome, Vagas = json.Vagas, PaisID = model.Pais };

                    _context.Cursos.Add(c);
                    tmp.Add(c);
                });
                
                var e = new Escola
                {
                    Email = model.Email,
                    Estado = model.Estado,
                    Nome = model.Nome,
                    PaisID = model.Pais,
                    Cursos = tmp,
                    TipoMobilidadeID = model.Mobilidade
                };

                var result = _context.Escolas.Add(e);

                if (result.State.ToString() == "Added")
                {
                    _context.SaveChanges();

                    _context.Cursos.ToList().ForEach(item => {
                        if (tmp.Contains(item))
                            item.EscolaID = e.EscolaID;
                    });

                    _context.SaveChanges();

                    _logger.LogInformation("User created a new escola parceira.");

                    return RedirectToAction(nameof(Index));
                }
            }

            model.PaisesList = GetPaises();
            model.MobilidadeList = GetMobilidade();
            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context);
            model.Cursos = new List<Curso>();

            return View(model);
        }

        /// <summary>
        /// Mostra a view de criação de escolas novamente mas desta vês
        /// preenche os camposdo dado da escola que estamos prestes a editar
        /// </summary>
        /// <param name="id">id da escola que queremos etidar</param>
        /// <returns>View</returns>
        [HttpGet]
        [Route("/Escolas/{id}/Edit")]
        public IActionResult Escolas(int id)
        {
            try
            {
                var tmp = _context.Escolas.Where(e => e.EscolaID == id).Include(e => e.Cursos).FirstOrDefault();
                var c = new List<string>();

                tmp.Cursos.ToList().ForEach(item => {
                    c.Add(JsonConvert.SerializeObject(new { item.Nome, item.Vagas }));
                });

                return View(new EscolasParceirasViewModel {
                    PaisesList = GetPaises(),
                    MobilidadeList = GetMobilidade(),
                    AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context),
                    Cursos = new List<Curso>(tmp.Cursos),
                    CursosNovos = new List<string>(),
                    Email = tmp.Email,
                    Nome = tmp.Nome,
                    Pais = tmp.PaisID,
                    Mobilidade = tmp.TipoMobilidadeID,
                    Estado = tmp.Estado
                });
            }
            catch (Exception)
            {
                return Json(new { status = "error", data = HelperFunctionsExtensions.GetError(("Unknown"), _context)});
            }
        }

        /// <summary>
        /// Valida se os campos obrigatórios têm valores. Se tiverem atualiza os campos e redireciona para a listagem de escolas,
        /// caso contrário devovle a mesma view com os respetivos erros 
        /// </summary>
        /// <param name="id">id da candidatura que estamos a editar</param>
        /// <param name="model">ViewModel relativo à criação / edição de escolas parceiras</param>
        /// <returns>RedirectToAction da view index ou view atual</returns>
        [HttpPost]
        [Route("/Escolas/{id}/Edit")]
        public IActionResult Escolas(EscolasParceirasViewModel model, int id)
        {
            var e = _context.Escolas.Where(es => es.EscolaID == id).Include(es => es.Cursos).FirstOrDefault();

            try
            {
                if (ModelState.IsValid)
                {
                    model.CursosNovos?.ForEach(c => {
                        var json = JsonConvert.DeserializeObject<Curso>(c);

                        e.Cursos.Add(new Curso { Nome = json.Nome, Vagas = json.Vagas, PaisID = model.Pais, EscolaID = id });
                    });

                    e.Email = model.Email;
                    e.Nome = model.Nome;
                    e.PaisID = model.Pais;
                    e.TipoMobilidadeID = model.Mobilidade;
                    e.Estado = model.Estado;

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var c = new List<string>();

                    e.Cursos.ToList().ForEach(item => {
                        c.Add(JsonConvert.SerializeObject(new { item.Nome, item.Vagas }));
                    });

                    model.Email = e.Email;
                    model.Nome = e.Nome;
                    model.Pais = e.PaisID;
                    model.Mobilidade = e.TipoMobilidadeID;
                    model.Estado = e.Estado;
                    model.PaisesList = GetPaises();
                    model.MobilidadeList = GetMobilidade();
                    model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context);
                    model.Cursos = e.Cursos.ToList();
                    model.CursosNovos = new List<string>();

                    return View(model);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", HelperFunctionsExtensions.GetError(("Unknown"), _context));

                model.PaisesList = GetPaises();
                model.MobilidadeList = GetMobilidade();
                model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context);
                model.Cursos = e.Cursos.ToList();
                model.CursosNovos = new List<string>();

                return View(model);
            }
        }

        /// <summary>
        /// Filtra a listagem de escolas parceiras por nome e / ou país
        /// </summary>
        /// <returns>Listagem de escolas correspondentes ao filtro em JSON</returns>
        // GET: Escolas/FilterEscola?nome=ze&pais=alemanha
        [HttpGet]
        [Route("/Escolas/FilterEscola")]
        public ActionResult FilterEscola()
        {
            try
            {
                var nome = Request.Query["nome"].Count == 0 ? "" : Request.Query["nome"][0].ToLower();
                var pais = Request.Query["pais"].Count == 0 ? "" : Request.Query["pais"][0];

                var res = _context.Escolas.Include(e => e.Cursos).Include(e => e.Pais).Include(e => e.TipoMobilidade).AsEnumerable().Where((e) => {
                    if (nome != "" && pais == "")
                        return e.Nome.ToLower().Contains(nome);

                    if (nome == "" && pais != "")
                        return e.PaisID == int.Parse(pais);

                    return e.Nome.ToLower().Contains(nome) && e.PaisID == int.Parse(pais);
                }).ToList();

                var tmp = new List<object>();

                res.ForEach(item => { tmp.Add(new {
                        nome = item.Nome,
                        pais = item.Pais.Descricao,
                        estado = item.Estado,
                        mobilidade = item.TipoMobilidade.Descricao,
                        id = item.EscolaID
                    });
                });

                return Json(tmp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err filter escolas: " + ex.Message);
                return StatusCode(500);
            }
        }



        /** HELPER FUNCTIONS **/
        /// <summary>
        /// Retorna uma lista com os paises
        /// </summary>
        /// <returns>List de Pais</returns>
        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        /// <summary>
        /// Retorna uma lista com os tipos de mobilidade
        /// </summary>
        /// <returns>List de TipoMobilidade</returns>
        private List<TipoMobilidade> GetMobilidade()
        {
            return _context.TiposMobilidade.ToList();
        }

        /// <summary>
        /// Retorna uma lista com as escolas pareceiras existentes
        /// </summary>
        /// <returns>List de Escola</returns>
        private List<Escola> GetEscolas()
        {
            return _context.Escolas.Include(e => e.TipoMobilidade).ToList();
        }
    }
}