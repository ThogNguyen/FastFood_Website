using Data.Models.AccountModels.Response;
using Data.Models.UserRolesModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpUserRoleService : IHttpUserRoleService
    {
        private readonly HttpClient _httpClient;

        public HttpUserRoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UserRoleForView>> GetAllUserRolesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/UserRolesApi/get-userroles");
            if (response.IsSuccessStatusCode)
            {
                var userRoles = await response.Content.ReadFromJsonAsync<IEnumerable<UserRoleForView>>();
                return userRoles ?? Enumerable.Empty<UserRoleForView>();
            }

            throw new ApplicationException($"Failed to retrieve user roles. Status code: {response.StatusCode}");
        }

        public async Task<UserRoleForView> GetUserRolesByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/UserRolesApi/get-userrole/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var userRole = await response.Content.ReadFromJsonAsync<UserRoleForView>();
                if (userRole != null)
                {
                    return userRole;
                }
            }

            throw new ApplicationException($"User role with ID {userId} not found. Status code: {response.StatusCode}");
        }

        public async Task UpdateUserRoleAsync(string userId, UserRoleForUpdate userRole)
        {
            string data = JsonConvert.SerializeObject(userRole);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/UserRolesApi/update-userrole/{userId}", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully updated user role with ID {userId}");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to update user role with ID {userId}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}
