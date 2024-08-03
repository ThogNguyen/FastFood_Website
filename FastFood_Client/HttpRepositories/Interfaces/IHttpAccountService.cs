using Data.Models.AccountModels.Response;
using Data.Models.AccountModels;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpAccountService
    {
        Task<BaseResponseMessage> Register(RegisterVM registerVM);
        Task<LoginVMResponse> Login(LoginVM loginVM);
        Task<BaseResponseMessage> ForgotPassword(ForgotPasswordVM forgotPasswordVM);
        Task<BaseResponseMessage> ResetPassword(ResetPasswordVM resetPasswordVM);
    }
}
