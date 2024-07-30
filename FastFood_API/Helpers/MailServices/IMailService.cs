using Data.Models.MailModels;

namespace FastFood_API.Helpers.MailServices
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
