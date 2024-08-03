using Data.Models.AccountModels;
using Data.Models.AccountModels.Response;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpAccountService : IHttpAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;

        public HttpAccountService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _baseAddress = apiSettings.Value.BaseAddress;
        }

        public async Task<BaseResponseMessage> Register(RegisterVM registerVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/accounts/register", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponseMessage>(responseString);
        }

        public async Task<LoginVMResponse> Login(LoginVM loginVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/accounts/login", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginVMResponse>(responseString);
        }

        public async Task<BaseResponseMessage> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(forgotPasswordVM), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/accounts/forgot-password", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponseMessage>(responseString);
        }

        public async Task<BaseResponseMessage> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(resetPasswordVM), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseAddress}/api/accounts/reset-password", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponseMessage>(responseString);
        }
    }
}
