using Data.Models.AccountModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpAccountService
    {
        Task RegisterUserAsync(RegisterVM registerVM);
        Task LoginUserAsync(LoginVM loginVM);
        Task ForgotPasswordAsync(ForgotPasswordVM model);
        Task ResetPasswordAsync(ResetPasswordVM model);
    }
}
