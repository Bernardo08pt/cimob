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

        /// <summary>
        /// Define o texto de ajuda das páginas e devolve a página "inicial" dos serviços cimob
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [Route("[Controller]")]
        public IActionResult Index()
        {
            var tmp = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "ServicosCIMOB" }), _context);
            ViewData["HelpTitle"] = tmp["Pagina"].Titulo;
            ViewData["HelpContent"] = tmp["Pagina"].Corpo;
            return View();
        }

        /// <summary>
        /// Retorna a view da listagem de candidaturas
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Mostra o ecrã com os detalhes todos da candidatura com o id recebido. Se ocorrer algum 
        /// problema a obter a candidatura, é redireiconado para a listagem novamente
        /// </summary>
        /// <param name="id">id da candidatura que estamos à procura</param>
        /// <returns>View</returns>
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

                if (c.EstadoCandidaturaID == 1)
                {
                    UpdateEstado(id, 3);
                }
                
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

        /// <summary>
        /// Envia um email à instituição com o id recebido a pedir novas vagas para determinado curso
        /// </summary>
        /// <param name="id">id da instituição que queremos pedir vagas</param>
        /// <returns>Object Json com estado (success / error) e caso erro, mesangem de erro</returns>
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
                
                await EmailSenderExtensions.RequestVagas(_emailSender, tmp.escola, tmp.curso, tmp.email);

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

        /// <summary>
        /// Carrega um ficheiro pdf para o servidor
        /// </summary>
        /// <param name="file">ficheiro a ser carregado</param>
        /// <returns>Object JSON com estado e mensagem de erro / informação do documento carregaod</returns>
        [HttpPost]
        [Route("[Controller]/UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file)
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

        /// <summary>
        /// cria um novo registo na BD a ligar o novo documento inserido à candidatura e envia um email
        /// ao candidato a informar que foi carregado um novo documento
        /// </summary>
        /// <param name="model">ViewModel correspondente à candidatura onde vai buscar o ID da candidatura e do documento</param>
        /// <returns>status code</returns>
        [HttpPost]
        [Route("[Controller]/UpdateFile")]
        public async Task<ActionResult> UpdateFile(CandidaturaViewModel model)
        {
            try
            {
                _context.CandidaturaDocumentos.Add(new CandidaturaDocumentos {
                    CandidaturaID = model.ID,
                    DocumentoID = model.DocID
                });
                _context.SaveChanges();

                UpdateEstado(model.ID, 2);

                await EmailSenderExtensions.NovoDocumento(_emailSender, model.Email, model.Nome);

                return StatusCode(200);
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza a candidatura com uma nova data de entrevista e envia um email ao candidato 
        /// a informar da data de entrevsita
        /// </summary>
        /// <param name="model">ViweModel correspondente à candidatura</param>
        /// <returns>Objecto JSON com o resultado e mensagem de erro</returns>
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

                UpdateEstado(model.ID, 4);

                var user = _context.Users.Where(u => u.Id == id).Select(u => new { email = u.Email, nome = u.Nome }).FirstOrDefault();

                var tmp = model.Entrevista.Split(" ");
                var dia = tmp[0].Split("-");    

                await EmailSenderExtensions.EntrevistaMarcada(_emailSender, user.nome, dia, user.email, tmp);

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

        /// <summary>
        /// Verifica se houve alterações aos valores (pontuação, observações o listagme de avaliagem de ucs)
        /// Se ouve, tenta atualizar e retorna um objecto json com o resultado e mensagem de erro (se for o caos)
        /// Caso contrário devolve um obj json com estado e mensagem de informação
        /// </summary>
        /// <param name="model">ViewModel relativa à candidatura</param>
        /// <returns>object json</returns>
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

        /// <summary>
        /// Verifica se a candidatura tem pontuação e entrevista definida. Se tiver, atualiza 
        /// o estado para aceite / rejeitada e envia um email ao candidato. Caso contrário devolve um objeto json com mensagem de erro
        /// </summary>
        /// <param name="model">Viewmodel relativo à candidatura</param>
        /// <returns>objecto json com estado do pedido e possivel mensagem de erro</returns>
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

                UpdateEstado(model.ID, 5);

                var user = _context.Users.Where(u => u.Id == user_id).Select(u => new { email = u.Email, nome = u.UserName }).FirstOrDefault();
                
                await EmailSenderExtensions.Resultado(_emailSender, user.email, user.nome, (model.Estado == 1) ? "rejeitada. A razão da rejeição é: " + model.Razao : "Aceite!");
                
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
        
        /// <summary>
        /// Tenta descarregar o ficheiro com o id recebido
        /// </summary>
        /// <param name="id">id do ficheiro que queremos descarregar</param>
        /// <returns>O documento a descarregar, redirectToAction se não encontrou o documento</returns>
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
                return RedirectToAction(nameof(NoFile));
            }
        }

        /// <summary>
        /// Retorna a view que indica que não foi encontrado o documento procurado
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }

        /// <summary>
        /// Tenta devolver o ficheiro com o id recebido como pdf para visualização
        /// </summary>
        /// <param name="id">id do ficheiro que queremos visualizar</param>
        /// <returns>O ficheiro a visualizar, redirectToAction se não encontrou o ficheiro</returns>
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

        /// <summary>
        /// Funções auxiliares que devolvem as respetivas views
        /// </summary>
        private ActionResult Candidatura => View();
        private ActionResult CandidaturasList => View();

        /// <summary>
        /// Atualiza o estado de uma determinada candidatura
        /// </summary>
        /// <param name="id">id da candidatura a atualizar</param>
        /// <param name="estado">novo estado da candidatura</param>
        private void UpdateEstado(int id, int estado)
        {
            _context.Candidaturas.Where(cu => cu.CandidaturaID == id).ToList().ForEach(cu => cu.EstadoCandidaturaID = estado);
            _context.SaveChanges();
        }
    }
}