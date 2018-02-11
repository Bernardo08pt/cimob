using Xunit;
using cimob.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using cimob.Models;

namespace XUnitTests
{
    /// <summary>
    /// Classe com testes unitários sobre os tipos de mobilidade
    /// </summary>
    public class Mobilidade
    {
        private ProgramasMobilidadeController controller;
        
        public Mobilidade()
        {
            controller = new ProgramasMobilidadeController(null, Globals.Context);
        }

        /// <summary>
        /// Testa se o utilizador tem candidatura ou não
        /// O teste passa se o titulo dá página não for "Página dos Programas de Mobilidade", 
        /// ou seja, está na página do estado da candidatura e não na página de escolha de programa de mobilidade
        /// </summary>
        
        public void HasCandidatura()
        {
            //ViewResult res = controller.Index() as ViewResult;

            //Assert.NotEqual("Página dos Programas de Mobilidade", res.ViewData["HelpTitle"]);
        }
    }
}
