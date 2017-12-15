using cimob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any ajudas.
            if (context.Ajudas.Any())
            {
                return;   // DB has been seeded
            }

            var ajudas = new Ajuda[]
            {
               new Ajuda{Nome="Email", Titulo="Endereço Email", Corpo="<p>O Email é necessário para efetuar o login na aplicação.</p><p> Este deve ser validado através de um link enviado para o mesmo.</p>"},
               new Ajuda{Nome="Password", Titulo="Password", Corpo="<p>A Password é necessária para efetuar o login na aplicação.</p><p> Esta deve conter um mínimo de 6 caracteres.</p>"},
               new Ajuda{Nome="Numero", Titulo="Número", Corpo="<p>O Número corresponde ao número de aluno/docente do IPS.</p>"}
            };
            foreach (Ajuda a in ajudas)
            {
                context.Ajudas.Add(a);
            }
            context.SaveChanges();
        }
    }
}
