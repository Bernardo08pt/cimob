using Microsoft.VisualStudio.TestTools.UnitTesting;
using cimob.Controllers;
using cimob.Models;
using Microsoft.AspNetCore.Identity;
using cimob.Data;

namespace UnitTests
{
    [TestClass]
    public class CreateCandidatura
    {
        UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _context;

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void SelectTipoMobilide()
        {
            var controller = new ProgramasMobilidadeController(_userManager, _context);


        }

        [TestMethod]
        public void Candidatar()
        {

        }
    }
}
