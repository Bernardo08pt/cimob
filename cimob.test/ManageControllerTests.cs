using cimob.Controllers;
using cimob.Models.ManageViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace cimob.test
{
    public class ManageControllerTests
    {
        [Fact]
        public void Index()
        {
            ManageController m = new cimob.Controllers.ManageController(new Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser>);

            Task<IActionResult> result = m.Index();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            IndexViewModel model = Assert.IsType<IndexViewModel>(viewResult.Model);

            Assert.NotNull(model.Username);
        }
    }
}
