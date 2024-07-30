using Data.Models.CategoryModels;
using FastFood_Client.HttpRepositories.Interfaces;
using System.Net.Http;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpCategoryService : IHttpCategoryService
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CategoryForView>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/CategoriesApi/get-categories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryForView>>();
                if (categories != null)
                {
                    return categories;
                }
            }
            return Enumerable.Empty<CategoryForView>();
        }
    }
}
