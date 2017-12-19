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


           

            // Verificar se as ajudas já estão adicionadas
            if (context.Ajudas.Any())
            {
                return;
            }

            //Todas as ajudas
            var ajudas = new Ajuda[]
            {
                //Login
                new Ajuda{Pagina = "Registo", Nome="Pagina", Titulo="Página de Registo",
                   Corpo ="<p>A Página de Registo é onde pode criar a sua conta de forma a entrar na aplicação e realizar a sua candidatura.</p>" +
                          "<p>Para realizar o seu registo preencha os corretamente os dados do formulário. Após a submissão dos dados será enviado um Email com um link de confirmação, o qual precisa de ser acedido para validar a sua conta</p>"},
                new Ajuda{Pagina = "Registo", Nome="Numero", Titulo="Número", Corpo="<p>O Número corresponde ao número de aluno/docente do IPS.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Nome", Titulo="Nome", Corpo="<p>O seu nome, preferencialmente completo.</p>"},
                new Ajuda{Pagina = "Registo", Nome="DataNascimento", Titulo="Data de Nascimento", Corpo="<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Email", Titulo="Endereço Email", Corpo="<p>Insira um Email válido e único.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Password", Titulo="Password", Corpo="<p>A Password deve conter um mínimo de 6 caracteres.</p>"},
                new Ajuda{Pagina = "Registo", Nome="ConfirmPassword", Titulo="Confirmar Password", Corpo="<p>O campo Confirmar Password tem de ser igual à Password.</p>"},
                
               //Registo
               new Ajuda{Pagina = "Login", Nome="Pagina", Titulo="Página de Autenticação",
                   Corpo ="<p>A Página de Autenticação é onde pode introduzir os dados da sua conta de forma a entrar na aplicação. O Email tem de estar validado através do link de confirmação enviado para o mesmo.</p>" +
                          "<p>No caso de não possuir uma conta é necessário aceder à Página de Registo de forma a criar uma.</p>" +
                          "<p>Caso possua uma conta mas esqueceu-se da sua password, aceda à Página de Recuperação de password.</p>"},
               new Ajuda{Pagina = "Login", Nome="Email", Titulo="Endereço Email", Corpo="<p>O Email associado à sua conta.</p>"},
               new Ajuda{Pagina = "Login", Nome="Password", Titulo="Password", Corpo="<p>A Password da sua conta.</p>"},

               //Recuperar Password
               new Ajuda{Pagina = "RecuperarPassword", Nome="Pagina", Titulo="Página de Recuperação de Password",
                   Corpo ="<p>A Página de Recuperação de Password é onde pode fazer um pedido para recuperar a sua password no caso esquecimento da mesma.</p>" +
                          "<p>Para fazer o pedido insira o Email da sua conta no campo correspondente e submeta o formulário. Irá receber um email com um link para restaurar a sua password.</p>"},
               new Ajuda{Pagina = "RecuperarPassword", Nome="Email", Titulo="Endereço Email", Corpo="<p>O Email associado à sua conta.</p>"},

               //Alterar Password
               new Ajuda{Pagina = "AlterarPassword", Nome="Pagina", Titulo="Página de Alteração de Password",
                   Corpo ="<p>A Página de Alteração de Password é onde pode alterar a sua password no caso de ter pedido para recuperar a mesma.</p>" +
                          "<p>Para alterar a passord insira o Email da sua conta, uma nova password e submeta o formulário.</p>"},
               new Ajuda{Pagina = "AlterarPassword", Nome="Email", Titulo="Endereço Email", Corpo="<p>O Email associado à sua conta.</p>"},
               new Ajuda{Pagina = "AlterarPassword", Nome="Password", Titulo="Password", Corpo="<p>Escolha uma nova Password.</p><p>Tem de conter um mínimo de 6 caracteres.</p>"},
               new Ajuda{Pagina = "AlterarPassword", Nome="ConfirmPassword", Titulo="Confirmar Password", Corpo="<p>O campo Confirmar Password tem de ser igual à Password.</p>"},
           };

            //Adicionar as ajudas à db
            foreach (Ajuda a in ajudas)
            {
                context.Ajudas.Add(a);
            }

            //Gravar alterações
            context.SaveChanges();
        }
    }
}
