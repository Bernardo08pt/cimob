using cimob.Data;
using Microsoft.EntityFrameworkCore;

namespace XUnitTests
{
    /// <summary>
    /// Classe auxiliar que contém variáveis globais necessárias a todos os testes
    /// </summary>
    public class Globals
    {
        /// <summary>
        /// ConnectionString da DataBase
        /// </summary>
        public static string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-cimob-E8511382-8E83-4730-950F-9EE68A039297;Trusted_Connection=True;MultipleActiveResultSets=true";
        /// <summary>
        /// Context da aplicação (i.e.: variável de ligação à BD)
        /// </summary>
        public static ApplicationDbContext Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ConnectionString).Options);
    }
}