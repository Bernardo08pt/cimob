using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using cimob.Services;

namespace cimob.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirmação de Email",
                $"Para confirmar o seu Email clique: <a href='{HtmlEncoder.Default.Encode(link)}'>aqui</a>");
        }
    }
}
