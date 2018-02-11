using Xunit;
using cimob.Controllers;
using cimob.Data;
using Microsoft.AspNetCore.Identity;
using cimob.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace XUnitTests
{
    public class Candidatura
    {
        [Fact]
        public void SelectProgramaMobilidadeAsync()
        {
            var a = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            
            var controller = new ProgramasMobilidadeController(null, a);
            
        }
    }
}
