﻿using Data.Models.RoleModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpRoleService : IHttpRoleService
    {
        private readonly HttpClient _httpClient;

        public HttpRoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateRoleAsync(RoleForCreate roleForCreate)
        {
            string data = JsonConvert.SerializeObject(roleForCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/RolesApi/create-role", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to create role. Status code: {response.StatusCode}, Error: {responseContent}");
            }
        }

        public async Task DeleteRoleAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44346/api/RolesApi/delete-role/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to delete role with ID {id}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<IEnumerable<RoleForView>> GetAllRolesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/RolesApi/get-roles");
            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content.ReadFromJsonAsync<IEnumerable<RoleForView>>();
                return roles ?? Enumerable.Empty<RoleForView>();
            }

            return Enumerable.Empty<RoleForView>();
        }

        public async Task<RoleForView> GetRoleByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/RolesApi/get-role/{id}");
            if (response.IsSuccessStatusCode)
            {
                var role = await response.Content.ReadFromJsonAsync<RoleForView>();
                if (role != null)
                {
                    return role;
                }
            }

            throw new ApplicationException($"Role with ID {id} not found. Status code: {response.StatusCode}");
        }

        public async Task UpdateRoleAsync(string id, RoleForUpdate roleForUpdate)
        {
            string data = JsonConvert.SerializeObject(roleForUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/RolesApi/update-role/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to update role with ID {id}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}