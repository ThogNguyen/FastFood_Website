using Data.Models.AccountModels.Response;
using Data.Models.AccountModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponseMessage> RegisterAsync(RegisterVM registerVM);
        Task<LoginVMResponse> LoginAsync(LoginVM loginVM);
        Task<BaseResponseMessage> ForgotPasswordAsync(ForgotPasswordVM model);
        Task<BaseResponseMessage> ResetPasswordAsync(ResetPasswordVM model);
    }
}
