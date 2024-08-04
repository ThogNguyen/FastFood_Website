using Data.Models.AccountModels.Response;
using Data.Models.AccountModels;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpAccountService
    {
        Task RegisterUserAsync(RegisterVM registerVM);
        Task LoginUserAsync(LoginVM loginVM);
        Task ForgotPasswordAsync(ForgotPasswordVM model);
        Task ResetPasswordAsync(ResetPasswordVM model);
    }
}
