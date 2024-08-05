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

        public Task CreateCategory(CategoryForCreate model)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryForView>> GetAllCategory()
        {
            throw new NotImplementedException();
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

        public Task<CategoryForView> GetCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(CategoryForUpdate model)
        {
            throw new NotImplementedException();
        }
    }
}
