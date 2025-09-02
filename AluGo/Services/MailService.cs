using MimeKit;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Options; // <- evita colisã

namespace AluGo.Services
{
    public class SmtpEmailOptions
    {
        public string FromName { get; set; } = "Sua Empresa";
        public string FromEmail { get; set; } = "no-reply@suaempresa.com";
        public string Host { get; set; } = "smtp.seuprovedor.com";
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string User { get; set; } = "usuario";
        public string Password { get; set; } = "senha";
    }

    public class MailKitEmailService : IEmailService
    {
        private readonly SmtpEmailOptions _opt;
        public MailKitEmailService(IOptions<SmtpEmailOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(string to, string subject, string htmlBody, EmailAttachment[] attachments = null)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_opt.FromName, _opt.FromEmail));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlBody };

            if (attachments != null)
            {
                foreach (var a in attachments)
                    builder.Attachments.Add(a.FileName, a.Content, ContentType.Parse(a.ContentType));
            }

            msg.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_opt.Host, _opt.Port, _opt.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
            await client.AuthenticateAsync(_opt.User, _opt.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
