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

            if (!context.IpsCursos.Any())
            {
                AddIpsCursosESTS(context);
                context.SaveChanges();

                AddIpsCursosESTB(context);
                context.SaveChanges();

                AddIpsCursosESS(context);
                context.SaveChanges();

                AddIpsCursosESE(context);
                context.SaveChanges();

                AddIpsCursosESCE(context);
                context.SaveChanges();
            }

            if (context.EstadosCandidatura.Count() == 0)
            {
                AddEstadosCandidatura(context);
                context.SaveChanges();
            }

            if (!context.Erros.Any())
            {
                AddErros(context);
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
                new IpsCurso { Nome = "Engenharia Biomédica", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Engenharia de Automação, Controlo e Instrumentação", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Engenharia do Ambiente", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Engenharia Eletrotécnica e de Computadores", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Engenharia Informática", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Engenharia Mecânica", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Tecnologia Biomédica", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Tecnologias de Energia", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Tecnologias do Ambiente e do Mar", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Licenciatura em Tecnologia e Gestão Industrial", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Energia", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Engenharia Biomédica - Desporto e Reabilitação", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Engenharia de Produção", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Engenharia de Software Novo", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Engenharia Eletrotécnica e de Computadores", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Informática de Gestão", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Mestrado em Segurança e Higiene no Trabalho", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Motorização de Veículos Elétricos e Híbridos", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Eficiência Energética e Energias Renováveis em Edíficios", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Engenharia da Instrumentação e Sistemas de Automação", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Engenharia Informática", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Lean Operations Management", IpsEscolaID = 1 },
                new IpsCurso { Nome = "Pós-Graduação em Tecnologia Aeronáutica", IpsEscolaID = 1 },
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
                new IpsCurso { Nome = "Bioinformática", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Biotecnologia", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Licenciatura em Engenharia Civil", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Engenharia Química", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Gestão da Construção", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Tecnologias do Petróleo", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Conservação e Reabilitação do Edificado", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Engenharia Biológica e Química", IpsEscolaID = 2 },
                new IpsCurso { Nome = "Mestrado em Engenharia Civil", IpsEscolaID = 2 },
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
                new IpsCurso { Nome = "Acupuntura", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Enfermagem", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Fisioterapia", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Terapia da Fala", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Pós-Licenciatura de Especialização em Enfermagem Médico-Cirúrgica", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Pós-Licenciatura de Especialização em Enfermagem de Saúde Mental e Psiquiatria", IpsCursoID = 3 },
                new IpsCurso { Nome = "Pós-Licenciatura de Especialização em Enfermagem Médico-Cirúrgica", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Enfermagem do Trabalho", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Saúde Sexual e Reprodutiva: Mutilação Genital Feminina", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Enfermagem", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Enfermagem Perioperatória", IpsEscolaID = 3 },
                new IpsCurso { Nome = "Fisioterapia", IpsEscolaID = 3 }
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
                new IpsCurso { Nome = "Animação e Intervenção Sociocultural", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Comunicação Social", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Desporto", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Educação Básica", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Tradução e Interpretação de Língua Gestual Portuguesa", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Língua Gestual Portuguesa", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Mestrado em Educação Pré-Escolar", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Mestrado em Educação Pré-Escolar e Ensino do 1º ciclo do Ensino Básico", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Mestrado em Ensino do 1º Ciclo do Ensino Básico e de Matemática e Ciências Naturais do 2º Ciclo do Ensino Básico", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Mestrado em Ensino do 1º ciclo do Ensino Básico e de Português e História e Geografia de Portugal no 2º ciclo do Ensino Básico", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Mestrado em Gestão e Administração de Escolas ", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Pós-Graduação em Educação Especial - Domínio Cognitivo e Motor", IpsEscolaID = 4 },
                new IpsCurso { Nome = "Pós-Graduação em Supervisão Pedagógica e Formação de Formadores", IpsEscolaID = 4 }
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
                new IpsCurso { Nome = "Licenciatura em Contabilidade e Finanças", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Contabilidade e Finanças Noturno", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão da Distribuição e da Logística", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão da Distribuição e da Logística Pós-Laboral", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão de Recursos Humanos", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão de Recursos Humanos Pós-Laboral", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Licenciatura em Gestão de Sistemas de Informação", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Marketing", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Ciências Empresariais ", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Mestrado em Contabilidade e Finanças", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão de Marketing", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Mestrado em Gestão de Sistemas de Informação", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão e Administração de Escolas", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão Estratégica de Recursos Humanos", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Segurança e Higiene no Trabalho", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Contabilidade Pública", IpsEscolaID = 5 },
                new IpsCurso { Nome = "Gestão e Marketing Turístico", IpsEscolaID = 5 },
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
            //Login
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "Pagina", Titulo = "Página de Registo", Corpo = "<p>A Página de Registo é onde pode criar a sua conta de forma a entrar na aplicação e realizar a sua candidatura.</p><p>Para realizar o seu registo preencha os corretamente os dados do formulário. Após a submissão dos dados será enviado um Email com um link de confirmação, o qual precisa de ser acedido para validar a sua conta</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "Numero", Titulo = "Número", Corpo = "<p>O Número corresponde ao número de aluno/docente do IPS.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "Nome", Titulo = "Nome", Corpo = "<p>O seu nome, preferencialmente completo.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "DataNascimento", Titulo = "Data de Nascimento", Corpo = "<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p><p>O candidato tem de ter entre 17 a 100 anos.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "Email", Titulo = "Endereço Email", Corpo = "<p>Insira um Email válido e único.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "Password", Titulo = "Password", Corpo = "<p>A Password deve seguir as seguintes regras: </p><ul><li>Conter um mínimo de 6 caracteres.</li><li>Conter pelo menos 1 dígito.</li><li>Conter pelo menos 1 caracter especial ['@' '.' '_' ect...].</li></ul>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Registo", Nome = "ConfirmPassword", Titulo = "Confirmar Password", Corpo = "<p>O campo Confirmar Password tem de ser igual à Password.</p>" });

            //Registo
            context.Ajudas.Add(new Ajuda { Pagina = "Login", Nome = "Pagina", Titulo = "Página de Autenticação", Corpo = "<p>A Página de Autenticação é onde pode introduzir os dados da sua conta de forma a entrar na aplicação. O Email tem de estar validado através do link de confirmação enviado para o mesmo.</p><p>No caso de não possuir uma conta é necessário aceder à Página de Registo de forma a criar uma.</p><p>Caso possua uma conta mas esqueceu-se da sua password, aceda à Página de Recuperação de password.</p>"});
            context.Ajudas.Add(new Ajuda { Pagina = "Login", Nome = "Email", Titulo = "Endereço Email", Corpo = "<p>O Email associado à sua conta.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Login", Nome = "Password", Titulo = "Password", Corpo = "<p>A Password da sua conta.</p>" });

            //Recuperar Password
            context.Ajudas.Add(new Ajuda { Pagina = "RecuperarPassword", Nome = "Pagina", Titulo = "Página de Recuperação de Password",                Corpo = "<p>A Página de Recuperação de Password é onde pode fazer um pedido para recuperar a sua password no caso esquecimento da mesma.</p><p>Para fazer o pedido insira o Email da sua conta no campo correspondente e submeta o formulário. Irá receber um email com um link para restaurar a sua password.</p>"});
            context.Ajudas.Add(new Ajuda { Pagina = "RecuperarPassword", Nome = "Email", Titulo = "Endereço Email", Corpo = "<p>O Email associado à sua conta.</p>" });

            //Alterar Password
            context.Ajudas.Add(new Ajuda { Pagina = "AlterarPassword", Nome = "Pagina", Titulo = "Página de Alteração de Password", Corpo = "<p>A Página de Alteração de Password é onde pode alterar a sua password no caso de ter pedido para recuperar a mesma.</p><p>Para alterar a passord insira o Email da sua conta, uma nova password e submeta o formulário.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "AlterarPassword", Nome = "Email", Titulo = "Endereço Email", Corpo = "<p>O Email associado à sua conta.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "AlterarPassword", Nome = "Password", Titulo = "Password", Corpo = "<p>Insira uma nova Password.</p><p>A Password deve seguir as seguintes regras: </p><ul><li>Conter um mínimo de 6 caracteres.</li><li>Conter pelo menos 1 dígito.</li><li>Conter pelo menos 1 caracter especial ['@' '.' '_' ect...].</li></ul>" });
            context.Ajudas.Add(new Ajuda { Pagina = "AlterarPassword", Nome = "ConfirmPassword", Titulo = "Confirmar Password", Corpo = "<p>O campo Confirmar Password tem de ser igual à Password.</p>" });

            // candidatura
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Pagina", Titulo = "Página Candidatura", Corpo = "<p>Esta págna permite-lhe submeter uma nova candidatura. Para tal é necessário preencher informação geral sobre si, contactos, submeter a carta de motivação e escolher até 3 possíveis destinos.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ApplicationState", Nome = "Pagina", Titulo = "Página Estado da Candidatura", Corpo = "<p>Esta página permite-lhe consultar o estado atual da sua candidatura.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "PaginaDocs", Titulo = "Página Documentos da Candidatura", Corpo = "<p>Esta página permite-lhe consultar todos os documentos associados à sua candidatura.</p>" });

            // candidatura - geral
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Numero", Titulo = "Número", Corpo = "<p>O Número corresponde ao número de aluno/docente do IPS.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Nome", Titulo = "Nome", Corpo = "<p>O seu nome, preferencialmente completo.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "DataNascimento", Titulo = "Data de Nascimento", Corpo = "<p>A Data de Nascimento tem de ser escrita no formato dd/mm/aaaa.</p><p>O candidato tem de ter entre 17 a 100 anos.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Escola", Titulo = "Escola", Corpo = "<p>A escola que frequenta atualmente.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Curso", Titulo = "Curso", Corpo = "<p>O curso que frequenta atualmente.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "AnoLetivo", Titulo = "Número", Corpo = "<p>Ano letivo atual. Este campo é preenchdio automáticamente.</p>" });

            // candidatura - contactos
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Email", Titulo = "Email", Corpo = "<p>Email através do qual possa ser contacto.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "EmailAlternativo", Titulo = "Email Alternativo", Corpo = "<p>Email alternativo caso haja algum problema com o principal.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "ContactoPessoal", Titulo = "Contacto Pessoal", Corpo = "<p>O seu contacto telefonico pessoal.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "ContactoEmergencia", Titulo = "Contacto de Emergência", Corpo = "<p>Contacto telefonico em caso de algum emergência.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Parentesco", Titulo = "Parentesco", Corpo = "<p>Parentesco do contacto de emergência.</p>" });

            // candidatura - documentação
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Nome", Titulo = "Nome", Corpo = "<p>Nome da escola a procurar.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Pais", Titulo = "País", Corpo = "<p>País sobre o qual quer saber que escolas / cursos existem.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "CartaMotivacao", Titulo = "Carta de Motivação", Corpo = "<p>Carta de motivação da candidatura, em inglês e em format PDF.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Application", Nome = "Documento", Titulo = "Documento", Corpo = "<p>Novo documento a ser inseiro a pedido do CIMOB, em format PDF.</p>" });

            //inserção de editais
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "NomeEdital", Titulo = "Nome do edital", Corpo = "<p>O nome do edital que vai submeter.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "CarregarEdital", Titulo = "Carregar edital", Corpo = "<p>O ficheiro tem de ser em formato PDF.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "SelecionarPrograma", Titulo = "Selecionar Programa", Corpo = "<p>Selecione o programa a que se refere o edital.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "DataLimite", Titulo = "Data Limite", Corpo = "<p>Data em que o edital \"Fecha\", deixando de aceitar candidaturas. Esta data não pode ser inferior ou igual à atual.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "PaginaList", Titulo = "Página editais", Corpo = "<p>Esta págna permite-lhe consultar os editais existentes e criar novos. Através do \"Lápis\" na tabela, consegue editar um edital.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "PaginaEdicao", Titulo = "Página edição editais", Corpo = "<p>Esta págna permite-lhe editar o PDF e a data limite de um edital.</p>" });
            
            //visualização dos editais do lado do candidato
            context.Ajudas.Add(new Ajuda { Pagina = "Editais", Nome = "Pagina", Titulo = "Página de editais", Corpo = "<p>Esta págna permite-lhe visualizar e fazer download dos editais submetidos pela CIMOB</p>" });

            // escolas parceiras
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Nome", Titulo = "Nome", Corpo = "<p>Nome da instituição com a qual o IPS tem acordo bilateral</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "NomeCurso", Titulo = "Nome do Curso", Corpo = "<p>Nome do Curso existente na instituição em questão</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Vagas", Titulo = "Vagas", Corpo = "<p>Número de vagas que o curso tem para estudantes do IPS</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Pais", Titulo = "País", Corpo = "<p>País da instituição em questão</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Email", Titulo = "Email", Corpo = "<p>Email da instituição em questão. Meio de comunicação entre o CIMOB-IPS e a instituição" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "NomeFilter", Titulo = "Nome", Corpo = "<p>Nome de instituições a pesquisar.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "PaisFilter", Titulo = "País", Corpo = "<p>País de instituições a pesquisar.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Estado", Titulo = "Estado", Corpo = "<p>Indica se esta escola atualmente é uma opção para os candidatos.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "PaginaList", Titulo = "Página de listagem das escolas parceiras", Corpo = "<p>Esta página permite-lhe visualizar e editar todas as escolas parceiras existentes bem como adicionar uma nova através do botão adicionar escola. </p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EscolasParceiras", Nome = "Pagina", Titulo = "Página criação / edição de escolas parceiras", Corpo = "<p>Esta página permite-lhe adicionar uma nova escola (bem como os respetivos cursos) ou editar uma escola já existente.</p>" });


            // serviços cimob
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "Pagina", Titulo = "Página de Serviços de CIMOB", Corpo = "<p>A página de editar de Serviços CIMOB é a página central do funcionário do CIMOB. É através desta página que consegue aceder às candidaturas submetidas, adicionar novas escolas parceiras e inserir / atualizar editais. </p>" });


            // servicos cimob - candidatura 
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "UC", Titulo = "Unidade Curricular", Corpo = "<p>Unidade Curricular do candidato.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "criterio", Titulo = "Critério de avaliação", Corpo = "<p>Critério de avaliação da UC.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "razao", Titulo = "Motivo Rejeição", Corpo = "<p>Motivo pelo qual esta candidatura está a ser recusada.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "Documento", Titulo = "Novo Documento", Corpo = "<p>Novo documento a adicionar a esta candidatura. Tem que ser formato PDF." });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCimob", Nome = "Pontuacao", Titulo = "Pontuação", Corpo = "<p>Pontuação que quer dar à candidatura submetida</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCimob", Nome = "Observacoes", Titulo = "Observações", Corpo = "<p>Informação adicional sobre a candidatura submetida</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "PaginaEscolas", Titulo = "Página criação / edição de escolas parceiras", Corpo = "<p>Esta págna permite-lhe adicionar uma nova escola (bem como os respetivos cursos) ou editar uma escola já existente.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "ServicosCIMOB", Nome = "PaginaCandidaturas", Titulo = "Página Candidaturas", Corpo = "Esta página permite-lhe consultar candidaturas submetidas e (através do separador Avaliação) interagir com a mesma, marcando uma data para entrevista, adicionando observações, definir a pontuação e aceitar ou recusar a candidatura.</p>" });

            
            // editar perfil
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "Pagina", Titulo = "Página de Editar Perfil", Corpo = "<p>A página de editar perfil é onde pode alterar o email e / ou password que utiliza para fazer a autenticação. </p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "Numero", Titulo = "Número", Corpo = "<p>Número de aluno / docente do IPS. Este campo não é editável. </p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "Nome", Titulo = "Nome", Corpo = "O seu nome. Este campo não é editável. </p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "Email", Titulo = "Email", Corpo = "O seu email. Este campo não é editável.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "OldPW", Titulo = "Password atual", Corpo = "<p>Corresponde à password que usa atualmente.</p>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "NewPW", Titulo = "Nova Password", Corpo = "<p>A nova password que deseja utilizar.</p><p>A Password deve seguir as seguintes regras: </p><ul><li>Conter um mínimo de 6 caracteres.</li><li>Conter pelo menos 1 dígito.</li><li>Conter pelo menos 1 caracter especial ['@' '.' '_' ect...].</li></ul>" });
            context.Ajudas.Add(new Ajuda { Pagina = "EditarPerfil", Nome = "ConfPW", Titulo = "Confirmação da Nova Password", Corpo = "<p>Por questões de segurança, escreva a nova password que deseja novamente.</p>" });

           

        }

        private static void AddEstadosCandidatura(ApplicationDbContext context)
        {
            context.EstadosCandidatura.Add(new EstadoCandidatura { Descricao = "Submetida", Cor = "#FBAF1C", Icon = "arrow-circle-o-right" });
            context.EstadosCandidatura.Add(new EstadoCandidatura { Descricao = "Enviar Documentação", Cor = "#08B69F", Icon = "paper-plane-o" });
            context.EstadosCandidatura.Add(new EstadoCandidatura { Descricao = "Confirmar Documentação", Cor = "#DB1A5D", Icon = "file-text" });
            context.EstadosCandidatura.Add(new EstadoCandidatura { Descricao = "Entrevista", Cor = "#2A3B91", Icon = "users" });
            context.EstadosCandidatura.Add(new EstadoCandidatura { Descricao = "Finalizada", Cor = "#8DD2C5", Icon = "check" });

            //Gravar alterações
            context.SaveChanges();
        }

        private static void AddErros(ApplicationDbContext context)
        {
            context.Erros.Add(new Erro { Nome = "Unknown", Mensagem = "Ocorreu um erro inesperado! Se presistir por favor contacte um administrador.", Codigo = 1 });
            context.Erros.Add(new Erro { Nome = "FileTooBig", Mensagem = "Tamanho do ficheiro tem que ser inferior a 1mb.", Codigo = 2 });
            context.Erros.Add(new Erro { Nome = "InvalidFile", Mensagem = "Ficheiro inserido inválido, tente outro ficheiro.", Codigo = 3 });
            context.Erros.Add(new Erro { Nome = "InvalidFormat", Mensagem = "Ficheiro tem que ter o formato pdf.", Codigo = 4 });
            context.Erros.Add(new Erro { Nome = "EditalExists", Mensagem = "Atenção: Já existe um edital com esse nome e tipo de mobilidade.", Codigo = 5 });
            context.Erros.Add(new Erro { Nome = "EmailEscolasParceiras", Mensagem = "Erro a enviar email à escola parceira. Se o problema persistir considere enviar um email manualmente.", Codigo = 6 });
            context.Erros.Add(new Erro { Nome = "UpdateCandidatura", Mensagem = "Ocorreu um erro inesperado a atualizar a Candidatura. Por favor tente mais tarde!", Codigo = 7 });
            context.Erros.Add(new Erro { Nome = "InvalidCandidaturaState", Mensagem = "Não é possível aceitar uma candidatura sem fazer a intervista e com 0 pontuação!", Codigo = 8 });
            context.Erros.Add(new Erro { Nome = "NoNewData", Mensagem = "Não houve alterações nenhumas à candidatura.", Codigo = 9 });

            //Gravar alterações
            context.SaveChanges();
        }
    }
}