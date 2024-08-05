using Data.Models.UserModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FastFoodWeb_Client.HttpRepositories.Services
{
    public class HttpUserService : IHttpUserService
    {
        private readonly HttpClient _httpClient;

        public HttpUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UserForView>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/UsersApi/get-users");
            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadFromJsonAsync<IEnumerable<UserForView>>();
                return roles ?? Enumerable.Empty<UserForView>();
            }

            return Enumerable.Empty<UserForView>();
        }

        public async Task<UserForView> GetUserByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/UsersApi/get-user/{id}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserForView>();
                if (user != null)
                {
                    return user;
                }
            }

            throw new ApplicationException($"User with ID {id} not found. Status code: {response.StatusCode}");
        }

        public async Task UpdateUserAsync(string id, UserForUpdate userDto)
        {
            string data = JsonConvert.SerializeObject(userDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/UsersApi/update-user/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully updated user role with ID {id}");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to update user role with ID {id}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}
