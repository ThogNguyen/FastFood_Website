using Data.Models.CategoryModels;
using FastFood_Client.HttpRepositories.Interfaces;
using System.Net.Http.Json;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpCategoryService : IHttpCategoryService
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryForView>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7241/api/CategoriesApi/get-categories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryForView>>();
                return categories;
            }
            return Enumerable.Empty<CategoryForView>();
        }
    }
}