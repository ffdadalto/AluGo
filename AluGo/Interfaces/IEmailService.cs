public interface IEmailService
{
    Task SendAsync(string to, string subject, string htmlBody, EmailAttachment[] attachments = null);
}

public sealed record EmailAttachment(string FileName, byte[] Content, string ContentType);
