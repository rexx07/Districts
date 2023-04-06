namespace Infrastructure.Mailing;

public interface IMailService
{
    void SendMail(Mail mail);
    Task SendEmailAsync(Mail mail);
}