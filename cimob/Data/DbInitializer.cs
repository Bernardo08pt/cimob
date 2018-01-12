using cimob.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cimob.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Verificar se as ajudas já estão adicionadas
            if (!context.Parentescos.Any())
            {
                AddParentesco(context);
            }

            if (!context.Paises.Any())
            {
                AddPaises(context);
            }

            if (!context.TiposMobilidade.Any())
            {
                AddTipoMobilidade(context);
            }

            if (!context.Ajudas.Any())
            {
                AddAjudas(context);
            }

            if (!context.Escolas.Any())
            {
                AddEscolas(context);
            }

            if (!context.Cursos.Any())
            {
                AddCursos(context);
            }

            if (!context.IpsEscolas.Any())
            {
                AddIpsEscolas(context);
            }

            if (!context.IpsCursos.Any())
            {
                AddIpsCursos(context);
            }
        }

        /** data feed functions **/
        private static void AddParentesco(ApplicationDbContext context)
        {
            context.Parentescos.Add(new Parentesco { Descricao = "Pai "});
            context.Parentescos.Add(new Parentesco { Descricao = "Mãe" });
            context.Parentescos.Add(new Parentesco { Descricao = "Irmã(o)" });
            context.Parentescos.Add(new Parentesco { Descricao = "Primo(a)" });
            context.Parentescos.Add(new Parentesco { Descricao = "Tio(a)" });
            context.Parentescos.Add(new Parentesco { Descricao = "Avô(ó)" });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddTipoMobilidade(ApplicationDbContext context)
        {
            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 1,
                Descricao = "Erasmus +",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 2,
                Descricao = "Erasmus +",
                Estagio = 1
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 3,
                Descricao = "Mobilidade Vasco da Gama",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 4,
                Descricao = "Bolsas Luso-Brasileiras Santander Universidades",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 5,
                Descricao = "Bolsas Ibero-Americanas Santander Universidades",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                TipoMobilidadeID = 6,
                Descricao = "Cooperação com o Instituto Politécnico de Macau",
                Estagio = 0
            });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddPaises(ApplicationDbContext context)
        {
            context.Paises.Add(new Pais { Descricao = "Alemanha" });
            context.Paises.Add(new Pais { Descricao = "França" });
            context.Paises.Add(new Pais { Descricao = "Polónia" });
            context.Paises.Add(new Pais { Descricao = "Bélgica" });
            context.Paises.Add(new Pais { Descricao = "Espanha" });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddIpsCursos(ApplicationDbContext context)
        {
            // ests
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 1, Nome = "Engenharia Biomédica" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 2, Nome = "Engenharia de Automação, Controlo e Instrumentação" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 3, Nome = "Licenciatura em Engenharia do Ambiente" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 4, Nome = "Licenciatura em Engenharia Eletrotécnica e de Computadores" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 5, Nome = "Licenciatura em Engenharia Informática" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 6, Nome = "Licenciatura em Engenharia Mecânica" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 7, Nome = "Licenciatura em Tecnologia Biomédica" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 8, Nome = "Licenciatura em Tecnologias de Energia" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 9, Nome = "Licenciatura em Tecnologias do Ambiente e do Mar" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 10, Nome = "Licenciatura em Tecnologia e Gestão Industrial" });

            // estb
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 11, Nome = "Bioinformática" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 12, Nome = "Biotecnologia" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 13, Nome = "Engenharia Civil" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 14, Nome = "Engenharia Química" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 15, Nome = "Gestão da Construção" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 16, Nome = "Tecnologias do Petróleo" });

            // ess
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 17, Nome = "Acupuntura" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 18, Nome = "Enfermagem" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 19, Nome = "Fisioterapia" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 20, Nome = "Terapia da Fala" });

            // ese
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 21, Nome = "Animação e Intervenção Sociocultural" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 22, Nome = "Comunicação Social" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 23, Nome = "Desporto" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 24, Nome = "Educação Básica" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 25, Nome = "Tradução e Interpretação de Língua Gestual Portuguesa" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 26, Nome = "Língua Gestual Portuguesa" });

            // esce
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 27, Nome = "Contabilidade e Finanças" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 28, Nome = "Contabilidade e Finanças Noturno" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 29, Nome = "Gestão da Distribuição e da Logística" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 30, Nome = "Gestão da Distribuição e da Logística Pós-Laboral" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 31, Nome = "Gestão de Recursos Humanos" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 32, Nome = "Gestão de Recursos Humanos Pós-Laboral" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 33, Nome = "Gestão de Sistemas de Informação" });
            context.IpsCursos.Add(new IpsCurso { IpsCursoID = 34, Nome = "Marketing" });

            //Gravar alterações
            context.SaveChanges();
        }
        
        private static void AddIpsEscolas(ApplicationDbContext context)
        {
            context.IpsEscolas.Add(new IpsEscola {
                Descricao = "Escola Superior de Tecnologia de Setúbal",
                Cursos = GetIpsCurso(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, context)
            });

            context.IpsEscolas.Add(new IpsEscola
            {
                Descricao = "Escola Superior de Tecnologia de Barreiro",
                Cursos = GetIpsCurso(new List<int> { 11, 12, 13, 14, 15, 16 }, context)
            });

            context.IpsEscolas.Add(new IpsEscola
            {
                Descricao = "Escola Superior de Saúde",
                Cursos = GetIpsCurso(new List<int> { 17, 18, 19, 20 }, context)
            });

            context.IpsEscolas.Add(new IpsEscola
            {
                Descricao = "Escola Superior de Educação",
                Cursos = GetIpsCurso(new List<int> { 21, 22, 23, 24, 25, 26 }, context)
            });

            context.IpsEscolas.Add(new IpsEscola
            {
                Descricao = "Escola Superior de Ciências Empresariais",
                Cursos = GetIpsCurso(new List<int> { 27, 28, 29, 30, 31, 32, 33, 34 }, context)
            });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddEscolas(ApplicationDbContext context)
        {
            context.Escolas.Add(new Escola {
                    TipoMobilidade = GetMobilidade(1, context),
                    Pais = GetPais(1, context),
                    Nome = "Escola fixolas",
                    Email = "Email da escola fixolas",
                    Estado = 1,
                    Cursos = GetCurso(new List<int>{ 1, 2, 3 }, context)
            });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddCursos(ApplicationDbContext context)
        {
            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Pais = GetPais(1, context),
                Nome = "Escola fixolas",
            });

            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Pais = GetPais(1, context),
                Nome = "Escola fixolas 2",
            });

            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Pais = GetPais(1, context),
                Nome = "Escola fixolas 3",
            });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddAjudas(ApplicationDbContext context)
        {
            //Todas as ajudas
            var ajudas = new Ajuda[]
            {
                //Login
                new Ajuda{Pagina = "Registo", Nome="Pagina", Titulo="Página de Registo",
                   Corpo ="<p>A Página de Registo é onde pode criar a sua conta de forma a entrar na aplicação e realizar a sua candidatura.</p>" +
                          "<p>Para realizar o seu registo preencha os corretamente os dados do formulário. Após a submissão dos dados será enviado um Email com um link de confirmação, o qual precisa de ser acedido para validar a sua conta</p>"},
                new Ajuda{Pagina = "Registo", Nome="Numero", Titulo="Número", Corpo="<p>O Número corresponde ao número de aluno/docente do IPS.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Nome", Titulo="Nome", Corpo="<p>O seu nome, preferencialmente completo.</p>"},
                new Ajuda{Pagina = "Registo", Nome="DataNascimento", Titulo="Data de Nascimento", Corpo="<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p><p>O candidato tem de ter entre 17 a 100 anos.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Email", Titulo="Endereço Email", Corpo="<p>Insira um Email válido e único.</p>"},
                new Ajuda{Pagina = "Registo", Nome="Password", Titulo="Password", Corpo="<p>A Password deve seguir as seguintes regras: </p><ul><li>Conter um mínimo de 6 caracteres.</li><li>Conter pelo menos 1 dígito.</li><li>Conter pelo menos 1 caracter especial ['@' '.' '_' ect...].</li></ul>"},
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
               new Ajuda{Pagina = "AlterarPassword", Nome="Password", Titulo="Password", Corpo="<p>Insira uma nova Password.</p><p>A Password deve seguir as seguintes regras: </p><ul><li>Conter um mínimo de 6 caracteres.</li><li>Conter pelo menos 1 dígito.</li><li>Conter pelo menos 1 caracter especial ['@' '.' '_' ect...].</li></ul>"},
               new Ajuda{Pagina = "AlterarPassword", Nome="ConfirmPassword", Titulo="Confirmar Password", Corpo="<p>O campo Confirmar Password tem de ser igual à Password.</p>"},

               // candidatura - geral
               new Ajuda{Pagina = "Application", Nome="Numero", Titulo="Número", Corpo="<p>O Número corresponde ao número de aluno/docente do IPS.</p>"},
               new Ajuda{Pagina = "Application", Nome="Nome", Titulo="Nome", Corpo="<p>O seu nome, preferencialmente completo.</p>"},
               new Ajuda{Pagina = "Application", Nome="DataNascimento", Titulo="Data de Nascimento", Corpo="<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p><p>O candidato tem de ter entre 17 a 100 anos.</p>"},
               new Ajuda{Pagina = "Application", Nome="Escola", Titulo="Escola", Corpo="<p>A escola que frequenta atualmente.</p>"},
               new Ajuda{Pagina = "Application", Nome="Curso", Titulo="Número", Corpo="<p>O curso que frequenta atualmente.</p>"},
               new Ajuda{Pagina = "Application", Nome="Ano", Titulo="Número", Corpo="<p>O ano atual no curso (matrícula atual).</p>"},

               // candidatura - contactos
               new Ajuda{Pagina = "Application", Nome="Email", Titulo="Email", Corpo="<p>Email através do qual possa ser contacto.</p>"},
               new Ajuda{Pagina = "Application", Nome="EmailAlternativo", Titulo="Email Alternativo", Corpo="<p>Email alternativo caso haja algum problema com o principal.</p>"},
               new Ajuda{Pagina = "Application", Nome="ContactoPessoal", Titulo="Contacto Pessoal", Corpo="<p>O seu contacto telefonico pessoal.</p>"},
               new Ajuda{Pagina = "Application", Nome="ContactoEmergencia", Titulo="Contacto de Emergência", Corpo="<p>Contacto telefonico em caso de algum emergência.</p>"},
               new Ajuda{Pagina = "Application", Nome="Parentesco", Titulo="Parentesco", Corpo="<p>Parentesco do contacto de emergência.</p>"},

               // candidatura - documentação
               new Ajuda{Pagina = "Application", Nome="Nome", Titulo="Nome", Corpo="<p>Nome da escola a procurar.</p>"},
               new Ajuda{Pagina = "Application", Nome="Pais", Titulo="País", Corpo="<p>País sobre o qual quer saber que escolas / cursos existem.</p>"},
               new Ajuda{Pagina = "Application", Nome="CartaMotivacao", Titulo="Carta de Motivação", Corpo="<p>Carta de motivação da candidatura, em inglês e em format PDF.</p>"},
            };

            //Adicionar as ajudas à db
            foreach (Ajuda a in ajudas)
            {
                context.Ajudas.Add(a);
            }

            //Gravar alterações
            context.SaveChanges();
        }


        /** helper functions **/
        private static TipoMobilidade GetMobilidade(int id, ApplicationDbContext context)
        {
            return (TipoMobilidade)context.TiposMobilidade.Where(tp => tp.TipoMobilidadeID == id);
        }

        private static Pais GetPais(int id, ApplicationDbContext context)
        {
            return (Pais)context.Paises.Where(p => p.PaisID == id);
        }

        private static Curso GetCurso(int id, ApplicationDbContext context)
        {
            return (Curso)context.Cursos.Where(c => c.CursoID == id);
        }

        private static List<Curso> GetCurso(List<int> id, ApplicationDbContext context)
        {
            return context.Cursos.Where(c => id.Contains(c.CursoID)).ToList();
        }

        private static IpsCurso GetIpsCurso(int id, ApplicationDbContext context)
        {
            return (IpsCurso)context.IpsCursos.Where(c => c.IpsCursoID == id);
        }

        private static List<IpsCurso> GetIpsCurso(List<int> id, ApplicationDbContext context)
        {
            return context.IpsCursos.Where(c => id.Contains(c.IpsCursoID)).ToList();
        }
    }
}
