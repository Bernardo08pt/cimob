using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.ServicosCimobViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class ServicosCimobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ServicosCimobController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Route("[Controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[Controller]/Candidaturas")]
        public IActionResult Candidaturas()
        {
            return View(nameof(CandidaturasList), new CandidaturasListingViewModel {
                Candidaturas = _context.Candidaturas.
                                Include(c => c.Utilizador).
                                Include(c => c.TipoMobilidade).
                                Include(c => c.IpsCurso).
                                Include(c => c.IpsCurso.IpsEscola).
                            ToList()
            });
        }

        [HttpGet]
        [Route("[Controller]/Candidaturas/{id}")]
        public async Task<ActionResult> ViewCandidatura(int id)
        {
            try
            {
                var c = _context.Candidaturas.
                            Include(cu => cu.Cursos).
                            Include(cu => cu.IpsCurso).
                            Include(cu => cu.IpsCurso.IpsEscola).
                            Include(cu => cu.EmegerenciaParentesco).
                            Where(cu => cu.CandidaturaID == id).
                        FirstOrDefault();

                var cursos = new Dictionary<int, Dictionary<string, int>>();
                var docs = new List<Documento>();
                var user = await _userManager.GetUserAsync(User);
                var escola = "";
                var pais = "";

                c.Cursos?.ToList().ForEach(item => {
                    _context.Cursos.
                        Where(cu => cu.CursoID == item.CursoID).
                        Include(cu => cu.Escola).
                        Include(cu => cu.Pais).
                    ToList().ForEach(curr => {
                        cursos.Add(item.CursoID, new Dictionary<string, int> { { curr.Nome, curr.Vagas } });
                        escola = curr.Escola.Nome;
                        pais = curr.Pais.Descricao;
                    });
                });

                return View(nameof(Candidatura), new CandidaturaViewModel {
                    AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                    AnoLetivo = c.AnoLetivo,
                    ContactoEmergencia = c.AnoLetivo,
                    ContactoPessoal = c.ContactoPessoal,
                    Curso = c.IpsCurso.Nome,
                    CursoDestino = cursos,
                    Documentos = docs,
                    Email = user.Email,
                    EmailAlternativo = c.EmailAlternativo,
                    Entrevista = c.Entrevista,
                    Nome = user.Nome,
                    Escola = c.IpsCurso.IpsEscola.Descricao,
                    NomeEscola = escola,
                    Numero = user.Numero,
                    Observacoes = c.Observacoes,
                    Pais = pais,
                    Parentesco = c.EmegerenciaParentesco.Descricao,
                    Pontuacao = c.Pontuacao,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("err gettting candidtura: " + ex.Message);
                return View(nameof(CandidaturasList), new CandidaturasListingViewModel {
                    Candidaturas = _context.Candidaturas.
                                Include(c => c.Utilizador).
                                Include(c => c.TipoMobilidade).
                                Include(c => c.IpsCurso).
                                Include(c => c.IpsCurso.IpsEscola).
                            ToList()
                });
            }
        }

        private ActionResult Candidatura => View();
        private ActionResult CandidaturasList => View();
    }
}