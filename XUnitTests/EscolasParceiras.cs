using Xunit;
using cimob.Controllers;
using cimob.Data;
using cimob.Models.EscolasParceirasViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTests
{
    public class EscolasParceiras
    {
        private DbContextOptions<ApplicationDbContext> options;
        private EscolasParceirasController controller;

        public EscolasParceiras() {
            options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(DBConnection.ConnectionString).Options;
            controller = new EscolasParceirasController(null, null, null, new ApplicationDbContext(options));
        }


        [Fact]
        public void HasEscolas()
        {
            ViewResult res = controller.Index() as ViewResult;
            Assert.NotEmpty(((EscolasListViewModel)res.ViewData.ModelExplorer.Model).EscolasList);
        }

        [Fact]
        public void AddEscola()
        {
            //var escola = new Escola
            //{
            //    PaisID = 1,
            //    TipoMobilidadeID = 1,
            //    Nome = "Escola fixolas",
            //    Email = "Email da escola fixolas",
            //    Estado = 1,
            //};

            //var c1 = new Curso {
            //    Vagas = 5,
            //    Nome = "Curso fixolas",
            //    EscolaID = 1,
            //    PaisID = 1
            //};

            //var c2 = new Curso
            //{
            //    Vagas = 5,
            //    Nome = "Curso fixolas 2",
            //    EscolaID = 1,
            //    PaisID = 1
            //};

            //var c3 = new Curso
            //{
            //    Vagas = 5,
            //    Nome = "Curso fixolas 3",
            //    EscolaID = 1,
            //    PaisID = 1
            //};
        }
    }
}
