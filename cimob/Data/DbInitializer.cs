using cimob.Models;
using Microsoft.EntityFrameworkCore;
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
                context.SaveChanges();
            }

            if (!context.Paises.Any())
            {
                AddPaises(context);
                context.SaveChanges();
            }
            
            if (!context.TiposMobilidade.Any())
            {
                AddTipoMobilidade(context);
                context.SaveChanges();
            }

            if (!context.Ajudas.Any())
            {
                AddAjudas(context);
                context.SaveChanges();
            }

            if (!context.Escolas.Any())
            {
                AddEscolas(context);
                context.SaveChanges();
            }

            if (!context.Cursos.Any())
            {
                AddCursos(context);
                context.SaveChanges();
            }

            if (!context.IpsEscolas.Any())
            {
                AddIpsEscolas(context);
                context.SaveChanges();
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
                Descricao = "Erasmus +",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                Descricao = "Erasmus +",
                Estagio = 1
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                Descricao = "Mobilidade Vasco da Gama",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                Descricao = "Bolsas Luso-Brasileiras Santander Universidades",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                Descricao = "Bolsas Ibero-Americanas Santander Universidades",
                Estagio = 0
            });

            context.TiposMobilidade.Add(new TipoMobilidade
            {
                Descricao = "Cooperação com o Instituto Politécnico de Macau",
                Estagio = 0
            });
        }

        private static void AddPaises(ApplicationDbContext context)
        {
            context.Paises.Add(new Pais { Descricao = "Alemanha" });
            context.Paises.Add(new Pais { Descricao = "França" });
            context.Paises.Add(new Pais { Descricao = "Polónia" });
            context.Paises.Add(new Pais { Descricao = "Bélgica" });
            context.Paises.Add(new Pais { Descricao = "Espanha" });
        }

        private static List<IpsCurso> AddIpsCursosESTS(ApplicationDbContext context)
        {
            var cursos = new List<IpsCurso>{
                new IpsCurso { Nome = "Engenharia Biomédica" },
                new IpsCurso { Nome = "Engenharia de Automação, Controlo e Instrumentação" },
                new IpsCurso { Nome = "Licenciatura em Engenharia do Ambiente" },
                new IpsCurso { Nome = "Licenciatura em Engenharia Eletrotécnica e de Computadores" },
                new IpsCurso { Nome = "Licenciatura em Engenharia Informática" },
                new IpsCurso { Nome = "Licenciatura em Engenharia Mecânica" },
                new IpsCurso { Nome = "Licenciatura em Tecnologia Biomédica" },
                new IpsCurso { Nome = "Licenciatura em Tecnologias de Energia" },
                new IpsCurso { Nome = "Licenciatura em Tecnologias do Ambiente e do Mar" },
                new IpsCurso { Nome = "Licenciatura em Tecnologia e Gestão Industrial" }
            };

            foreach (IpsCurso c in cursos)
            {
                context.IpsCursos.Add(c);
            }

            return cursos;
        }

        private static List<IpsCurso> AddIpsCursosESTB(ApplicationDbContext context)
        {
            var cursos = new List<IpsCurso>{
                new IpsCurso { Nome = "Bioinformática" },
                new IpsCurso { Nome = "Biotecnologia" },
                new IpsCurso { Nome = "Engenharia Civil" },
                new IpsCurso { Nome = "Engenharia Química" },
                new IpsCurso { Nome = "Gestão da Construção" },
                new IpsCurso { Nome = "Tecnologias do Petróleo" }
            };

            foreach (IpsCurso c in cursos)
            {
                context.IpsCursos.Add(c);
            }

            return cursos;
        }

        private static List<IpsCurso> AddIpsCursosESS(ApplicationDbContext context)
        {
            var cursos = new List<IpsCurso>{
                new IpsCurso { Nome = "Acupuntura" },
                new IpsCurso { Nome = "Enfermagem" },
                new IpsCurso { Nome = "Fisioterapia" },
                new IpsCurso { Nome = "Terapia da Fala" }
            };

            foreach (IpsCurso c in cursos)
            {
                context.IpsCursos.Add(c);
            }

            return cursos;
        }

        private static List<IpsCurso> AddIpsCursosESE(ApplicationDbContext context)
        {
            var cursos = new List<IpsCurso>{
                new IpsCurso { Nome = "Animação e Intervenção Sociocultural" },
                new IpsCurso { Nome = "Comunicação Social" },
                new IpsCurso { Nome = "Desporto" },
                new IpsCurso { Nome = "Educação Básica" },
                new IpsCurso { Nome = "Tradução e Interpretação de Língua Gestual Portuguesa" },
                new IpsCurso { Nome = "Língua Gestual Portuguesa" },
            };

            foreach (IpsCurso c in cursos)
            {
                context.IpsCursos.Add(c);
            }

            return cursos;
        }

        private static List<IpsCurso> AddIpsCursosESCE(ApplicationDbContext context)
        {
            var cursos = new List<IpsCurso>{
                new IpsCurso { Nome = "Contabilidade e Finanças" },
                new IpsCurso { Nome = "Contabilidade e Finanças Noturno" },
                new IpsCurso { Nome = "Gestão da Distribuição e da Logística" },
                new IpsCurso { Nome = "Gestão da Distribuição e da Logística Pós-Laboral" },
                new IpsCurso { Nome = "Gestão de Recursos Humanos" },
                new IpsCurso { Nome = "Gestão de Recursos Humanos Pós-Laboral" },
                new IpsCurso { Nome = "Gestão de Sistemas de Informação" },
                new IpsCurso { Nome = "Marketing" }
            };

            foreach (IpsCurso c in cursos)
            {
                context.IpsCursos.Add(c);
            }

            return cursos;
        }

        private static void AddIpsEscolas(ApplicationDbContext context)
        {
            context.IpsEscolas.Add(new IpsEscola { Descricao = "Escola Superior de Tecnologia de Setúbal" });

            context.IpsEscolas.Add(new IpsEscola { Descricao = "Escola Superior de Tecnologia de Barreiro" });

            context.IpsEscolas.Add(new IpsEscola { Descricao = "Escola Superior de Saúde" });

            context.IpsEscolas.Add(new IpsEscola { Descricao = "Escola Superior de Educação" });

            context.IpsEscolas.Add(new IpsEscola {Descricao = "Escola Superior de Ciências Empresariais" });
        }

        private static void AddEscolas(ApplicationDbContext context)
        {
            context.Escolas.Add(new Escola {
                    PaisID = 1,
                    TipoMobilidadeID = 1,
                    Nome = "Escola fixolas",
                    Email = "Email da escola fixolas",
                    Estado = 1
            });
        }

        private static void AddCursos(ApplicationDbContext context)
        {
            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Nome = "Curso fixolas",
                EscolaID = 1,
                PaisID = 1
            });

            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Nome = "Curso fixolas 2",
                EscolaID = 1,
                PaisID = 1
            });

            context.Cursos.Add(new Curso
            {
                Vagas = 5,
                Nome = "Curso fixolas 3",
                EscolaID = 1,
                PaisID = 1
            });
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

               //inserção de editais 
               new Ajuda{Pagina = "Editais", Nome="CarregarEdital", Titulo="Carregar edital", Corpo="<p>O ficheiro tem de ser em formato PDF.</p>"},

               //avaliação da candidatura 
               new Ajuda{Pagina = "VisualizarCandidatura", Nome="Pontuacao", Titulo="Pontuação", Corpo="<p>Pontuação que quer dar à candidatura submetida</p>"},
               new Ajuda{Pagina = "VisualizarCandidatura", Nome="Observacoes", Titulo="Observações", Corpo="<p>Informação adicional sobre a candidatura submetida</p>"}
               
            };

            //Adicionar as ajudas à db
            foreach (Ajuda a in ajudas)
            {
                context.Ajudas.Add(a);
            }
        }
    }
}

