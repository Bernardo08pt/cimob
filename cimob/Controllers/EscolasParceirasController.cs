using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.EscolasParceirasViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cimob.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class EscolasParceirasController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public EscolasParceirasController(
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
        [Route("/Escolas")]
        public IActionResult Index() {
            return View(new EscolasListViewModel {
                PaisesList = GetPaises(),
                EscolasList = GetEscolas(),
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context)
            });
        }

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

        [HttpPost]
        [Route("/Escolas/Create")]
        public IActionResult Escolas(EscolasParceirasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tmp = (model.Cursos == null ? new List<Curso>() : new List<Curso>(model.Cursos));

                model.CursosNovos.ForEach(item => {
                    var json = JsonConvert.DeserializeObject<Curso>(item);
                    var c = new Curso { Nome = json.Nome, Vagas = json.Vagas, PaisID = model.Pais };
                    _context.Cursos.Add(c);
                    _context.SaveChanges();
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

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context);
            model.PaisesList = GetPaises();
            model.MobilidadeList = GetMobilidade();

            return View(model);
        }


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

                return View(new EscolasParceirasViewModel
                {
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
                return View(nameof(Index));
            }
        }


        [HttpPost]
        [Route("/Escolas/{id}/Edit")]
        public IActionResult Escolas(EscolasParceirasViewModel model, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // update Escolas e Cursos
                    _context.Escolas.ToList().ForEach(item => {
                        // apaga os cursos da escola para voltar a meter
                        _context.Cursos.ToList().RemoveAll(c => c.EscolaID == item.EscolaID);
                    
                        var tmp = (model.Cursos == null ? new List<Curso>() : new List<Curso>(model.Cursos));

                        model.CursosNovos.ForEach(curso => {
                            var json = JsonConvert.DeserializeObject<Curso>(curso);

                            tmp.Add(new Curso { Nome = json.Nome, Vagas = json.Vagas, PaisID = model.Pais });
                        });

                        item.Email = model.Email;
                        item.Nome = model.Nome;
                        item.PaisID = model.Pais;
                        item.TipoMobilidadeID = model.Mobilidade;
                        item.Estado = model.Estado;
                        item.Cursos = tmp;
                    });   
                }
                else
                {
                    var tmp = _context.Escolas.Where(e => e.EscolaID == id).Include(e => e.Cursos).FirstOrDefault();
                    var c = new List<string>();

                    tmp.Cursos.ToList().ForEach(item => {
                        c.Add(JsonConvert.SerializeObject(new { item.Nome, item.Vagas }));
                    });

                    model.PaisesList = GetPaises();
                    model.MobilidadeList = GetMobilidade();
                    model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "EscolasParceiras" }), _context);
                    model.Cursos = new List<Curso>(tmp.Cursos);
                    model.CursosNovos = new List<string>();
                    model.Email = tmp.Email;
                    model.Nome = tmp.Nome;
                    model.Pais = tmp.PaisID;
                    model.Mobilidade = tmp.TipoMobilidadeID;
                    model.Estado = tmp.Estado;
                }

                return View(model);
            }
            catch (Exception)
            {
                return View(nameof(Index));
            }
        }

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
        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        private List<TipoMobilidade> GetMobilidade()
        {
            return _context.TiposMobilidade.ToList();
        }

        private List<Escola> GetEscolas()
        {
            return _context.Escolas.Include(e => e.TipoMobilidade).ToList();
        }
    }
}