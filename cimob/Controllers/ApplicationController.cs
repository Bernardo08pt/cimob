﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using cimob.Models;
using cimob.Models.EditaisViewModels;
using cimob.Services;
using cimob.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using cimob.Extensions;

namespace cimob.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationController(
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
        /// Devolve a View da candidatura com alguns dados relativos ao candidato já preenchidos (email, nome, número)
        /// </summary>
        /// <returns>Uma View que usa o ApplicationViewModel</returns>
        // GET: Application
        [Route("[controller]")]
        public async Task<ActionResult> Application()
        {
            if (HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User).User != null)
                return RedirectToAction(nameof(State));

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return View(new ApplicationViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                EscolasList = GetEscolas(),
                EscolaList = GetEscolasIPS(),
                CursoList = new List<IpsCurso>(),
                PaisesList = GetPaises(),
                ParentescoList = GetParentesco(),
                DataNascimento = user.DataNascimento,
                Numero = user.Numero,
                Nome = user.Nome,
                Email = user.Email
            });
        }
        
        /// <summary>
        /// Chamada quando o candidato submete a candidatura. Caso os campos estejam todos corretos 
        /// Cria um novo objeto Candidatura, guarda-o na BD e envia um email informativo ao candidato
        /// </summary>
        /// <param name="model">Corresponde ao ViewModel respetivo à candidatura</param>
        /// <returns>A view atual com erros caso haja algum campo incorreto, ou redirectToAction que redireciona para o estado da candidatura</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]")]
        public async Task<IActionResult> Application(ApplicationViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                var c = new Candidatura
                {
                    AnoLetivo = GetAnoLetivo(),
                    ContactoPessoal = model.ContactoPessoal,
                    DataNascimento = model.DataNascimento,
                    EmailAlternativo = model.EmailAlternativo,
                    EmegerenciaParentescoID = model.Parentesco,
                    EmergenciaContacto = model.ContactoEmergencia,
                    TipoMobilidadeID = model.TipoMobilidade,
                    Entrevista = "",
                    EstadoCandidaturaID = 1,
                    Estagio = 1,
                    IpsCursoID = model.Curso,
                    Observacoes = "",
                    Pontuacao = 0,
                    RejeicaoRazao = "",
                    Rejeitada = -1,
                    Semestre = GetSemestre(),
                    UtilizadorID = user.Id,
                    Documentos = null
                };

                var result = await _context.Candidaturas.AddAsync(c);
                
                if (result.State.ToString() == "Added")
                {
                    _context.SaveChanges();

                    var tmp = new List<CandidaturaCursos>();

                    model.SelectedCursos.Split(",").ToList().ForEach(item => {
                        tmp.Add(new CandidaturaCursos { CandidaturaID = c.CandidaturaID, CursoID = int.Parse(item) });
                    });
                    
                    _context.CandidaturaCursos.AddRange(tmp);

                    _context.SaveChanges();
                    
                    _context.CandidaturaDocumentos.Add(
                        new CandidaturaDocumentos {
                            CandidaturaID = c.CandidaturaID,
                            DocumentoID = model.DocID
                        });

                    _context.SaveChanges();

                    _logger.LogInformation("User created a new application.");

                    var mobilidade = _context.TiposMobilidade.Where(p => p.TipoMobilidadeID == model.TipoMobilidade).Select(p => p.Descricao).FirstOrDefault();

                    await EmailSenderExtensions.ApplicationSubmit(_emailSender, model.Email, mobilidade, user.Nome);
                    await EmailSenderExtensions.ApplicationSubmit(_emailSender, model.EmailAlternativo, mobilidade, user.Nome);

                    return RedirectToAction(nameof(State));
                }
            }

            model.AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context);
            model.EscolasList = GetEscolas();
            model.EscolaList = GetEscolasIPS();
            model.CursoList = new List<IpsCurso>();
            model.PaisesList = GetPaises();
            model.ParentescoList = GetParentesco();
            model.DataNascimento = user.DataNascimento;
            model.Numero = user.Numero;
            model.Nome = user.Nome;
            model.Email = user.Email;

            return View(model);
        }

        /// <summary>
        /// Mostra a view quando o url requisitado não é encontrado
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [Route("[controller]/Not-Found")]
        public IActionResult Not_Found()
        {
            return View();
        }

        /// <summary>
        /// Verifica se o user logado tem candidatura feita. Se não tiver redireciona para o notfound. Se tiver 
        /// retorna a view do estado da candidatura com informação do estado
        /// </summary>
        /// <returns>View do estado ou not found</returns>
        [HttpGet]
        [Route("[controller]/State")]
        public IActionResult State()
        {
            var tmp = HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User);

            if (tmp.User == null)
                return View(nameof(Not_Found));

            return View(new ApplicationStateViewModel {
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "ApplicationState" }), _context),
                EstadosList = GetEstadosCandidatura(),
                Estado = _context.Candidaturas.Where(c => c.CandidaturaID == tmp.Candidatura).Select(c => c.EstadoCandidaturaID).FirstOrDefault()
            });
        }

        /// <summary>
        /// Verifica se o user logado tem candidatura feita. Se não tiver redireciona para o notfound. Se tiver 
        /// retorna a view dos documentos da candidatura com os documentos
        /// </summary>
        /// <returns>View dos documentos ou not found</returns>
        [HttpGet]
        [Route("[controller]/Documents")]
        public IActionResult Documents()
        {
            var tmp = HelperFunctionsExtensions.GetUserCandidatura(_context, _userManager, User);

            if (tmp.User == null)
                return View(nameof(Not_Found));

            return View(new ApplicationDocumentsViewModel{
                AjudasDictionary = HelperFunctionsExtensions.GetAjudas(new List<string>(new string[] { "Application" }), _context),
                DocumentosList = GetDocumentosCandidatura(tmp.Candidatura),
            });
        }

        /// <summary>
        /// Vai buscar os cursos à BD que pertençam à escola do id recebido
        /// </summary>
        /// <param name="id">id da escola cujos cursos estamos à procura</param>
        /// <returns>Lista dos cursos da escola em JSON</returns>
        // GET: Application/GetCursosByEscola/1
        [HttpGet]
        public ActionResult GetCursosByEscola(int id)
        {
            return Json(_context.IpsCursos.Where(c => c.IpsEscolaID == id).ToList());
        }

        /// <summary>
        /// Filtra os destinos possíveis por nome ou país
        /// </summary>
        /// <returns>Retorna a lista com os países filtrados em JSON</returns>
        // GET: Application/FilterDestino?nome=ze&pais=alemanha
        [HttpGet]
        public ActionResult FilterDestino()
        {
            var nome = Request.Query["nome"].Count == 0 ? "" : Request.Query["nome"][0].ToLower();
            var pais = Request.Query["pais"].Count == 0 ? "" : Request.Query["pais"][0];

            var res = _context.Escolas.Include(e => e.Cursos).Include(e => e.Pais).AsEnumerable().Where((e) =>
            {
                if (nome != "" && pais == "")
                    return e.Nome.ToLower().Contains(nome);

                if (nome == "" && pais != "")
                    return e.PaisID == int.Parse(pais);

                return e.Nome.ToLower().Contains(nome) && e.PaisID == int.Parse(pais);
            }).ToList();

            var tmp = new List<object>();

            res.ForEach(item => {
                item.Cursos.ToList().ForEach(c => {
                    tmp.Add(new {
                        escola = item.Nome,
                        pais = item.Pais.Descricao,
                        curso = c.Nome,
                        id = c.CursoID
                    });
                });
            });
            
            return Json(tmp);
        }

        /// <summary>
        /// Recebe um ficheiro relativo à candidatura e carrega-o no servidor
        /// </summary>
        /// <param name="file">Ficheiro a ser carregado no ficheiro</param>
        /// <returns>Um object JSON com o resultado (error / success) e mensagem de erro / informação do documento, respetivamente</returns>
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var d = new Documento
                {
                    FicheiroCaminho = await FileHandling.Upload(file, "Candidaturas"),
                    FicheiroNome = file.FileName,
                    OrigemCimob = 0,
                    DataUpload = DateTime.Today

                };
                _context.Documentos.Add(d);
                _context.SaveChanges();

                return Json(new { status = "success", data = d });
            }
            catch (Exception ex)
            {
                return Json(
                    new {
                        status = "error",
                        data = HelperFunctionsExtensions.GetError((
                            ex is FileSizeException ? "FileTooBig" : 
                            ex is FormatException ? "InvalidFormat" : 
                            "InvalidFile"
                        ), _context)
                    }
                );
            }
        }

        /// <summary>
        /// Procura o ficheir com o ID recebido. Se existir, descarrega o ficheiro, caso contrário redireciona para uma view 
        /// de ficheiro não existente
        /// </summary>
        /// <param name="id">ID do ficheiro a procurar </param>
        /// <returns>O ficheiro a ser descarregado ou view</returns>
        // GET: Application/Download/1
        [HttpGet]
        public ActionResult Download (int id)
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
        /// Devolve a view de NoFile
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult NoFile()
        {
            return View();
        }

        /// <summary>
        /// Procura o ficheiro com o ID recebido. Se encontrar devolve (em pdf) para ser visivel no browser. Caso contrário
        /// mostra a view de NoFile
        /// </summary>
        /// <param name="id">ID do ficheiro que estamos a procurar</param>
        /// <returns>Ficheiro em PDF ou View</returns>
        // GET: Application/View/1
        [HttpGet]
        public ActionResult View (int id)
        {   
            try
            {
                return FileHandling.View(_context.Documentos.Where(d => d.DocumentoID == id).Select(d => d.FicheiroCaminho).FirstOrDefault());
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(NoFile));
            }
        }

        /** HELPER FUNCTIONS **/
        /// <summary>
        /// Vai buscar as escolas parceiras à BD que estão ativas e devolve como lista
        /// </summary>
        /// <returns>Listagem de Escola</returns>
        private List<Escola> GetEscolas()
        {
            return _context.Escolas.Include(e => e.Cursos).Where(e => e.Estado == 1).ToList();
        }

        /// <summary>
        /// Vai buscar as escolas do IPS à BD e devolve como lista
        /// </summary>
        /// <returns>Listagem de IpsEscola</returns>
        private List<IpsEscola> GetEscolasIPS()
        {
            return _context.IpsEscolas.ToList();
        }

        /// <summary>
        /// Vai buscar os cursos do IPS à BD e devolve como lista
        /// </summary>
        /// <returns>Listagem de IpsCurso</returns>
        private List<IpsCurso> GetCursoIPS()
        {
            return _context.IpsCursos.OrderBy(c => c.Nome).ToList();
        }

        /// <summary>
        /// Vai buscar os paises à BD e devolve como lista
        /// </summary>
        /// <returns>Listagem de Pais</returns>
        private List<Pais> GetPaises()
        {
            return _context.Paises.ToList();
        }

        /// <summary>
        /// Vai buscar o Parentesco à BD e devolve como lista
        /// </summary>
        /// <returns>Listagem de Parentesco</returns>
        private List<Parentesco> GetParentesco()
        {
            return _context.Parentescos.ToList();
        }

        /// <summary>
        /// Calcula o semestre atual consoante a data
        /// </summary>
        /// <returns>1 ou 2</returns>
        private short GetSemestre()
        {
            var mes = DateTime.Today.Month;
            return (short)((mes >= 8 && mes <= 2) ? 1 : 2);
        }

        /// <summary>
        /// Vai buscar à BD os estados que a candidatura pode ter
        /// </summary>
        /// <returns>Listagem CandidaturaDocumento</returns>
        private List<EstadoCandidatura> GetEstadosCandidatura()
        {
            return _context.EstadosCandidatura.ToList();
        }

        /// <summary>
        /// Vai buscar os documentos da candidatura como listage
        /// </summary>
        /// <param name="id">ID da candidatura que estamos à procura</param>
        /// <returns>Listagem CandidaturaDocumento</returns>
        private List<CandidaturaDocumentos> GetDocumentosCandidatura(int id)
        {
            return _context.CandidaturaDocumentos.Include(cd => cd.Documento).Where(cd => cd.CandidaturaID == id).ToList();
        }

        /// <summary>
        /// Calcula o ano letivo com base na data
        /// </summary>
        /// <returns>O ano letivo em string</returns>
        private string GetAnoLetivo()
        {
            var tmp = DateTime.Now;

            if (tmp.Month >= 8 && tmp.Month <= 12)
                return tmp.Year + "/" + (tmp.Year + 1);
            else
                return (tmp.Year - 1) + "/" + tmp.Year;
        }
    }
}