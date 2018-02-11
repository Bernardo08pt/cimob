using Xunit;
using cimob.Controllers;
using cimob.Models.EscolasParceirasViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using cimob.Data;
using Microsoft.Extensions.Logging;

namespace XUnitTests
{
    /// <summary>
    /// Classe com testes unitários sobre as escolas parceiras
    /// </summary>
    public class EscolasParceiras
    {
        private EscolasParceirasController controller;
        private ApplicationDbContext context;

        public EscolasParceiras() {
            context = Globals.Context;
            controller = new EscolasParceirasController(null, null, context);
        }

        /// <summary>
        /// Testa se existe alguma escola, sendo que por default existe sempre
        /// pelo menos uma escola. Este teste passa se a listagem de escolas não estiver vazia
        /// </summary>
        [Fact]
        public void HasEscolas()
        {
            ViewResult res = controller.Index() as ViewResult;
            Assert.NotEmpty(((EscolasListViewModel)res.ViewData.ModelExplorer.Model).EscolasList);
        }

        /// <summary>
        /// Testa a função de criar uma escola nova.
        /// O teste passa se depois de criar uma escola, o número 
        /// de escolas existentes é diferente do inicial
        /// </summary>
        [Fact]
        public void AddEscola()
        {
            var count = context.Escolas.CountAsync();

            controller.Escolas(new EscolasParceirasViewModel {
                Pais = 1,
                Mobilidade = 1,
                Nome = "Escola fixolas",
                Email = "Email da escola fixolas",
                Estado = 1,
                CursosNovos = new List<string>
                {
                    "{\"Nome\":\"teste 1\",\"Vagas\":1}",
                    "{\"Nome\":\"teste 2\",\"Vagas\":3}",
                }
            });

            Assert.NotEqual(count, context.Escolas.CountAsync());
        }
    }
}
