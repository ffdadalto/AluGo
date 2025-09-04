using MimeKit;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Options; // <- evita colisã

namespace AluGo.Services
{
    public class SmtpEmailOptions
    {
        public string FromName { get; set; } = "Franchescolle";
        public string FromEmail { get; set; } = "alugo@dadalto.dev.br";
        public string Host { get; set; } = "mail.smtp2go.com";
        public int Port { get; set; } = 2525;
        public bool UseSsl { get; set; } = false;
        public string User { get; set; } = "fdadalto";
        public string Password { get; set; } = "kZrpQNs7eygx4Zus";
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
