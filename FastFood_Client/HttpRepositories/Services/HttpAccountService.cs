using Data.Models.AccountModels;
using Data.Models.AccountModels.Response;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpAccountService : IHttpAccountService
    {
        private readonly HttpClient _httpClient;

        public HttpAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task ForgotPasswordAsync(ForgotPasswordVM model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/forgot-password", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to request password reset. Status code: {response.StatusCode}, Error: {responseContent}");
            }
        }

        public async Task LoginUserAsync(LoginVM loginVM)
        {
            string data = JsonConvert.SerializeObject(loginVM);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to login. Status code: {response.StatusCode}, Error: {responseContent}");
            }
        }

        public async Task RegisterUserAsync(RegisterVM registerVM)
        {
            string data = JsonConvert.SerializeObject(registerVM);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to register user. Status code: {response.StatusCode}, Error: {responseContent}");
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordVM model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/reset-password", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to reset password. Status code: {response.StatusCode}, Error: {responseContent}");
            }
        }
    }
}
