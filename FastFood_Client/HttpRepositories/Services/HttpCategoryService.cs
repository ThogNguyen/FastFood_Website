using Data.Models.CategoryModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpCategoryService : IHttpCategoryService
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryForView>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/CategoriesApi/get-categories"); // Thay đổi URL nếu cần
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryForView>>(content);
                return categories;
            }
            throw new ApplicationException("Unable to load categories.");
        }
        public async Task CreateCategory(CategoryForCreate model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/CategoriesApi/create-category", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<List<CategoryForView>> GetAllCategory()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/CategoriesApi/get-categories");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryForView>>(content)!;
                return categories;
            }
            throw new ApplicationException(content);
        }

        public async Task<CategoryForView> GetCategory(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/CategoriesApi/get-category/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<CategoryForView>(content)!;
                return category;
            }
            throw new ApplicationException("Category not found.");
        }

        public async Task UpdateCategory(CategoryForUpdate model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var putResult = await _httpClient.PutAsync("https://localhost:44346/api/CategoriesApi/update-category", content);
            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
