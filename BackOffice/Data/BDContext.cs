using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data
{ 
        public class BDContext : DbContext
        {
            public BDContext() : base(@"Server=tcp:cimobsql.database.windows.net,1433;Initial Catalog=cimob;Persist Security Info=False;User ID=administratorcimob;Password=esw12345.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            {

            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                Database.SetInitializer<DbContext>(null);
                modelBuilder.Entity<Role>().ToTable("AspNetRoles");
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
                base.OnModelCreating(modelBuilder);
            }
        
            public DbSet<Role> Roles { get; set; }
            public DbSet<ApplicationUser> Utilizadores { get; set; }
        }
    
}
