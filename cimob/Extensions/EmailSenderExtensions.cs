using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimob.Services;
using System.Net.Mail;
using System.IO;

namespace cimob.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string name, string link)
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
    }
}
