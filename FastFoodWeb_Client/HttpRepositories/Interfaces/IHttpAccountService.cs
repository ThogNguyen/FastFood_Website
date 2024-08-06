using Data.Models.AccountModels;
using Data.Models.AccountModels.Response;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpAccountService
    {
        Task RegisterUserAsync(RegisterVM registerVM);
        Task<LoginVMResponse> LoginUserAsync(LoginVM loginVM);
        Task Logout();
        Task ForgotPasswordAsync(ForgotPasswordVM model);
        Task ResetPasswordAsync(ResetPasswordVM model);
    }
}
