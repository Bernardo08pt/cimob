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
               //Inputs
               new Ajuda{Nome="Email", Titulo="Endereço Email", Corpo="<p>O Email é necessário para efetuar o login na aplicação.</p><p> Este deve ser validado através de um link enviado para o mesmo.</p>"},
               new Ajuda{Nome="Password", Titulo="Password", Corpo="<p>A Password é necessária para efetuar o login na aplicação.</p><p> Esta deve conter um mínimo de 6 caracteres.</p>"},
               new Ajuda{Nome="Numero", Titulo="Número", Corpo="<p>O Número corresponde ao número de aluno/docente do IPS.</p>"},
               new Ajuda{Nome="Nome", Titulo="Nome", Corpo="<p>O seu nome, preferencialmente completo.</p>"},
               new Ajuda{Nome="ConfirmPassword", Titulo="Confirmar Password", Corpo="<p>O campo Confirmar Password tem de ser igual à Password.</p>"},
               new Ajuda{Nome="DataNascimento", Titulo="Data de Nascimento", Corpo="<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p>"},

               //Páginas
               new Ajuda{Nome="Login", Titulo="Página de Autenticação",
                   Corpo ="<p>A Página de Autenticação é onde pode introduzir os dados da sua conta de forma a entrar na aplicação. O Email tem de estar validado através do link de confirmação enviado para o mesmo.</p>" +
                          "<p>No caso de não possuir uma conta é necessário aceder à Página de Registo de forma a criar uma.</p>" +
                          "<p>Caso possua uma conta mas esqueceu-se da sua password, aceda à Página de Recuperação de password.</p>"},
               new Ajuda{Nome="Registo", Titulo="Página de Registo",
                   Corpo ="<p>A Página de Registo é onde pode criar a sua conta de forma a entrar na aplicação e realizar a sua candidatura.</p>" +
                          "<p>Para realizar o seu registo preencha os corretamente os dados do formulário. Após a submissão dos dados será enviado um Email com um link de confirmação, o qual precisa de ser acedido para validar a sua conta</p>"},
               new Ajuda{Nome="RecuperarPassword", Titulo="Página de Recuperação de Password",
                   Corpo ="<p>A Página de Recuperação de Password é onde pode fazer um pedido para recuperar a sua password no caso esquecimento da mesma.</p>" +
                          "<p>Para fazer o pedido insira o Email da sua conta no campo correspondente e submeta o formulário. Irá receber um email com um link para restaurar a sua password.</p>"},
               new Ajuda{Nome="AlterarPassword", Titulo="Página de Alteração de Password",
                   Corpo ="<p>A Página de Redefinição de Password é onde pode alterar a sua password no caso de ter pedido para recuperar a mesma.</p>" +
                          "<p>Para alterar a passord insira o Email da sua conta, uma nova password e submeta o formulário.</p>"},
               /*
               new Ajuda{Nome="ConfirmacaoEmail", Titulo="Página de Aviso de Confirmação de Email",
                   Corpo ="<p>A Página de Aviso de Confirmação de Email é apenas informativa.</p>"},
               new Ajuda{Nome="ConfirmarRegisto", Titulo="Página de Aviso de Confirmar Registo",
                   Corpo ="<p>A Página de Aviso de Confirmar Registo é apenas informativa.</p>"},
               new Ajuda{Nome="AlterarPasswordConfirmacao", Titulo="Página de Aviso de Confirmação de Alteração de Password",
                   Corpo ="<p>A Página de Aviso de Confirmação de Alteração de Password é apenas informativa.</p>"},
               new Ajuda{Nome="RecuperarPasswordConfirmacao", Titulo="Página de Aviso de Confirmação de Recuperação de Password",
                   Corpo ="<p>A Página de Aviso de Confirmação de Recuperação de Password é apenas informativa.</p>"}
                */ 
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
