using cimob.Data;
using cimob.Extensions;
using cimob.Models;
using cimob.Models.ServicosCimobViewModels;
using cimob.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly IEmailSender _emailSender;
        
        public ServicosCimobController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
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
                            Include(cu => cu.Documentos).
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

                c.Documentos?.ToList().ForEach(item => {
                    _context.Documentos.
                        Where(doc => doc.DocumentoID == item.DocumentoID).
                    ToList().ForEach(curr => docs.Add(curr));
                });

                return View(nameof(Candidatura), new CandidaturaViewModel {
                    AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "ServicosCIMOB" }), _context),
                    AnoLetivo = c.AnoLetivo,
                    ContactoEmergencia = c.EmergenciaContacto,
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
                    Listagem = new List<UCAvaliacao>(),
                    NovaListagem = new List<string>(),
                    Estado = c.Rejeitada
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

        [HttpGet]
        [Route("[Controller]/Vagas/{id}")]
        public async Task<ActionResult> RequestVagas(int id)
        {
            try
            {
                var tmp = _context.Cursos.
                            Include(c => c.Escola).
                        Where(c => c.CursoID == id).
                        Select(c => new {
                            email = c.Escola.Email,
                            escola = c.Escola.Nome,
                            curso = c.Nome
                        }).
                        FirstOrDefault();

                var mensagem = "<p><span style='font-size: 18px;'>Dear " + tmp.escola + ",<strong> </strong></span></p>" +
                        "<p><span style='font-size: 18px;'>We've received more applications for " + tmp.curso + " than available vacancies and we were wondering if it's possible to allow more students?</span></p>" +
                        "<p><br></p>" +
                        "<p><span style = 'font-size: 18px;'> Best Regards CIMOB - IPS, Setúbal, Portugal </span></p>" +
                        "<p><span style = 'font-size: 14px;'> Note: This email was automatically generated so we ask that you reply to your regular email address instead of this one.</span></p>" +
                            "<span style = 'font-size: 12px;'> &nbsp;</span></p>";

                await _emailSender.SendEmailAsync(tmp.email, "Request for more vacancies", mensagem);

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("err sending email to escola parceira: " + ex.Message);
                return Json(new {
                    status = "error",
                    data = HelperFunctionsExtensions.GetError("EmailEscolasParceiras", _context)
                });
            }
        }

        [HttpPost]
        [Route("[Controller]/UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file, int id)
        {
            try
            {
                var d = new Documento {
                    FicheiroCaminho = await FileHandling.Upload(file, "Candidaturas"),
                    FicheiroNome = file.FileName,
                    OrigemCimob = 1,
                    DataUpload = DateTime.Today
                };

                _context.Documentos.Add(d);
                _context.SaveChanges();

                return Json(new { status = "success", data = d });
            }
            catch (Exception ex)
            {
                return Json(new {
                    status = "error",
                    data = HelperFunctionsExtensions.GetError((
                        ex is FileSizeException ? "FileTooBig" :
                        ex is FormatException ? "InvalidFormat" :
                        "InvalidFile"
                    ), _context)
                });
            }
        }

        [HttpPost]
        [Route("[Controller]/UpdateFile")]
        public ActionResult UpdateFile(CandidaturaViewModel model)
        {
            try
            {
                _context.CandidaturaDocumentos.Add(new CandidaturaDocumentos {
                    CandidaturaID = model.ID,
                    DocumentoID = model.DocID
                });
                _context.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        struct entrevista
        {
            public int id { get; set; }
            public string data { get; set; }
        }

        [HttpPost]
        [Route("[Controller]/Entrevista")]
        public async Task<ActionResult> MarcarEntrevista(CandidaturaViewModel model)
        {
            try
            {
                var id = "";
                _context.Candidaturas.
                        Where(c => c.CandidaturaID == model.ID).
                        ToList().ForEach(item => {
                            item.Entrevista = model.Entrevista;
                            id = item.UtilizadorID;
                        });

                _context.SaveChanges();

                var user = _context.Users.Where(u => u.Id == id).Select(u => new { email = u.Email, nome = u.Nome }).FirstOrDefault();

                var tmp = model.Entrevista.Split(" ");
                var dia = tmp[0].Split("-");    

                var mensagem = "<p><span style='font-size: 18px;'>Caro(a) " + user.nome + ",<strong> </strong></span></p>" +
                        "<p><span style='font-size: 18px;'>A entrevista relativa à sua mobilidade ficou marcada para dia " + dia[2] + "/" + dia[1] + "/" + dia[0] + " às " + tmp[1] + ".</span></p>" +
                        "<p><br></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos CIMOB - IPS</span></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                    "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                        "<span style = 'font-size: 12px;'> &nbsp;</span></p>";

                await _emailSender.SendEmailAsync(user.email, "Entrevista - Mobilidade", mensagem);

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("err sending email to escola parceira: " + ex.Message);
                return Json(new {
                    status = "error",
                    data = HelperFunctionsExtensions.GetError("EmailEscolasParceiras", _context)
                });
            }
        }

        [HttpPost]
        [Route("[Controller]/Avaliacao")]
        public ActionResult UpdateAvaliacao(CandidaturaViewModel model)
        {
            try
            {
                if (model.Pontuacao == 0 && model.Observacoes == null && model.NovaListagem.FirstOrDefault() == null)
                {
                    return Json(new {
                        status = "warning",
                        data = HelperFunctionsExtensions.GetError("NoNewData", _context)
                    });
                }


                var a = model.NovaListagem.FirstOrDefault(); 
                var tmp = model.Listagem == null ? new List<UCAvaliacao>() : new List<UCAvaliacao>(model.Listagem);

                model.NovaListagem.ForEach(item => {
                    var json = JsonConvert.DeserializeObject<UCAvaliacao>(item);
                    var uc = new UCAvaliacao { Criterio = json.Criterio, UC = json.UC };
                    _context.UCAvaliacao.Add(uc);
                    _context.SaveChanges();
                    tmp.Add(uc);
                });

                _context.Candidaturas.
                        Where(c => c.CandidaturaID == model.ID).
                        ToList().ForEach(item => {
                            item.Pontuacao = model.Pontuacao;
                            item.Observacoes = model.Observacoes;
                            item.UCAvaliacoes = tmp;
                        });

                _context.SaveChanges();

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("err updating candidatura: " + ex.Message);
                return Json(new {
                    status = "danger",
                    data = HelperFunctionsExtensions.GetError("UpdateCandidatura", _context)
                });
            }
        }

        [HttpPost]
        [Route("[Controller]/Resultado")]
        public async Task<ActionResult> SetResutado(CandidaturaViewModel model)
        {
            try
            {
                if (model.Entrevista == "" || model.Pontuacao == 0)
                {
                    return Json(new {
                        status = "error",
                        data = HelperFunctionsExtensions.GetError("InvalidCandidaturaState", _context)
                    });
                }

                var user_id = "";

                _context.Candidaturas.
                        Where(c => c.CandidaturaID == model.ID).
                        ToList().ForEach(item => {
                            item.Rejeitada = model.Estado;
                            item.RejeicaoRazao = model.Razao;
                            user_id = item.UtilizadorID;
                        });

                _context.SaveChanges();

                var user = _context.Users.Where(u => u.Id == user_id).Select(u => new { email = u.Email, nome = u.UserName }).FirstOrDefault();

                var resultado = (model.Estado == 1) ? "rejeitada. A razão da rejeição é: " + model.Razao : "Aceite!";

                var mensagem = "<p><span style='font-size: 18px;'>Resultado Candidatura " + user.nome + ",<strong> </strong></span></p>" +
                        "<p><span style='font-size: 18px;'>Gostaríamos de informar que a sua candidatura foi " + resultado + ".</span></p>" +
                        "<p><br></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos CIMOB - IPS</span></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                    "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                        "<span style = 'font-size: 12px;'> &nbsp;</span></p>";

                await _emailSender.SendEmailAsync(user.email, "Aplicação Mobilidade - Resultado", mensagem);


                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("err resultado candidatura: " + ex.Message);
                return Json(new {
                    status = "error",
                    data = HelperFunctionsExtensions.GetError("UpdateCandidatura", _context)
                });
            }
        }
        
        [HttpGet]
        [Route("[Controller]/Candidaturas/{id}/Download")]
        public ActionResult Download(int id)
        {
            try
            {
                var tmp = _context.Documentos.
                Where(d => d.DocumentoID == id).
                Select(d => new {
                    caminho = d.FicheiroCaminho,
                    nome = d.FicheiroNome
                }).FirstOrDefault();

                return FileHandling.Download(tmp.caminho, tmp.nome);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/NoFile.cshtml");
            }
        }

        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }
        
        [HttpGet]
        [Route("[Controller]/Candidaturas/{id}/View")]
        public ActionResult View(int id)
        {
            try
            {
                return FileHandling.View(_context.Documentos.Where(d => d.DocumentoID == id).Select(d => d.FicheiroCaminho).FirstOrDefault());
            }
            catch (Exception)
            {
                return View("~/Views/Shared/NoFile.cshtml");
            }
        }

        private ActionResult Candidatura => View();
        private ActionResult CandidaturasList => View();
    }
}