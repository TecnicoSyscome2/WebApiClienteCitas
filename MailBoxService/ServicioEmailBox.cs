using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Text;
namespace WebApi.Citas.ClientesApp.MailBoxService
{
    public class ServicioEmailBox
    {
        private readonly IConfiguration _configuration;

        public ServicioEmailBox(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarCorreoAsync(string destinatario, string asunto, string mensaje)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpClient = new SmtpClient
            {
                Host = smtpSettings["Host"],
                Port = int.Parse(smtpSettings["Port"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"]),
                Credentials = new NetworkCredential(smtpSettings["User"], smtpSettings["Password"])
            };

            var correo = new MailMessage
            {
                From = new MailAddress(smtpSettings["User"]),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true
            };

            correo.To.Add(destinatario);

            await smtpClient.SendMailAsync(correo);
        }
    }
}
