using cimob.Services;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace cimob.Extensions
{
    /// <summary>
    /// classe auxiliar com funções para enviar os emails
    /// </summary>
    public static class EmailSenderExtensions
    {
        internal static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string name, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmação de Email",
                "<p><span style='font-size: 18px;'>Caro(a) " + name + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'> O seu registo na plataforma de candidaturas CIMOB est&aacute; quase completo. Clique <a href='" + HtmlEncoder.Default.Encode(link) + "'>aqui</a> para ativar a sua conta e concluir o registo.&nbsp;</span></p>" +
                "<p><span style='font-size: 18px;'> Boa sorte no processo de candidaturas.&nbsp;</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                 "<span style = 'font-size: 12px;'> &nbsp;</span></p>");
        }

        internal static Task RecuperarPassword(this IEmailSender emailSender, string email, string nome, string link)
        {
            return emailSender.SendEmailAsync(email, "Recuperação de Password",
                "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>Recebemos um pedido de recuperação da palavra passe associada a este e-mail. Se não fez este pedido, ignore este e-mail (a sua conta continua segura).</span></p>" +
                "<p><span style='font-size: 18px;'>Caso contrário clique <a href='" + link + "'>aqui</a> para começar o processo de redefinição de palavra passe.&nbsp;</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                 "<span style = 'font-size: 12px;'> &nbsp;</span></p>");
        }

        internal static Task ApplicationSubmit(IEmailSender emailSender, string email, string mobilidade, string nome)
        {
            return emailSender.SendEmailAsync(email, "Candidatura efetuada",
                "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>Gostaríamos de informar que a sua candidatura para " + mobilidade + " foi efetuada com sucesso! Iremos avaliar assim que possível e mantêmo-lo-emos informado.</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }

        internal static Task EditalPublicado(IEmailSender emailSender, string email, string nome, string mobilidade)
        {
            return emailSender.SendEmailAsync(email, "Edital Publicado",
                    "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                    "<p><span style='font-size: 18px;'>Gostaríamos de informar que foi publicado o edital referente a " + mobilidade + ".</span></p>" +
                    "<p><br></p>" +
                    "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                    "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                        "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }

        internal static Task RequestVagas(IEmailSender emailSender, string escola, string curso, string email)
        {
            return emailSender.SendEmailAsync(email, "Request for more vacancies",
                "<p><span style='font-size: 18px;'>Dear " + escola + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>We've received more applications for " + curso + " than available vacancies and we were wondering if it's possible to allow more students?</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Best Regards CIMOB - IPS, Setúbal, Portugal </span></p>" +
                "<p><span style = 'font-size: 14px;'> Note: This email was automatically generated so we ask that you reply to your regular email address instead of this one.</span></p>" +
                    "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }

        internal static Task NovoDocumento(IEmailSender emailSender, string email, string nome)
        {
            return emailSender.SendEmailAsync(email, "Candidatura - Novo Documento",
                "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>Serve este email para informar que foi carregado um novo documento pela parte do CIMOB-IPS relativo à sua candidatura de mobilidade. Visite a documentação da candidatura na sua área pessoal da plataforma do CIMOB-IPS para poder visualizá-lo.</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }

        internal static Task EntrevistaMarcada(IEmailSender emailSender, string nome, string[] dia, string email, string[] hora)
        {
            return emailSender.SendEmailAsync(email, "Entrevista - Mobilidade",
                "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>A entrevista relativa à sua mobilidade ficou marcada para dia " + dia[2] + "/" + dia[1] + "/" + dia[0] + " às " + hora[1] + ".</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }

        internal static Task Resultado(IEmailSender emailSender, string email, string nome, string resultado)
        {
            return emailSender.SendEmailAsync(email, "Aplicação Mobilidade - Resultado",
                "<p><span style='font-size: 18px;'>Caro(a) " + nome + ",<strong> </strong></span></p>" +
                "<p><span style='font-size: 18px;'>Gostaríamos de informar que a sua candidatura foi " + resultado + ".</span></p>" +
                "<p><br></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos CIMOB - IPS</span></p>" +
                "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                "<span style = 'font-size: 12px;'> &nbsp;</span></p>"
            );
        }
    }
}