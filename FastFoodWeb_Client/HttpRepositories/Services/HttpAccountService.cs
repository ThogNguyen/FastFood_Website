using Blazored.LocalStorage;
using Data.Models.AccountModels;
using Data.Models.AccountModels.Response;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using FastFoodWeb_Client.Utility;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FastFoodWeb_Client.HttpRepositories.Services
{
    public class HttpAccountService : IHttpAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public HttpAccountService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService tokenStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = tokenStorageService;
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
        public async Task<LoginVMResponse> LoginUserAsync(LoginVM loginVM)
        {
            string data = JsonConvert.SerializeObject(loginVM);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to login. Status code: {response.StatusCode}, Error: {responseContent}");
            }

            var loginResult = JsonConvert.DeserializeObject<LoginVMResponse>(responseContent);

            // Store token in local storage
            await _localStorageService.SetItemAsync("authToken", loginResult!.Token);

            // Mark user as authenticated
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginVM.Username!);

            // Set default request headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");

            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
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

        public async Task<BaseResponseMessage> ResetPasswordAsync(ResetPasswordVM model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/AccountsApi/reset-password", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Something went wrong."
                };
            }
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Login Success."
            };
        }
    }
}
