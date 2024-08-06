using Data.Models.AccountModels.Response;
using Data.Models.CategoryModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FastFoodWeb_Client.HttpRepositories.Services
{
    public class HttpCategoryService : IHttpCategoryService
    {
        private readonly HttpClient _httpClient;

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategory(CategoryForCreate model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/CategoriesApi/create-category", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(postContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
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

        public async Task UpdateCategory(Guid id, CategoryForUpdate model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var putResult = await _httpClient.PutAsync("https://localhost:44346/api/CategoriesApi/put-category/" + id, content);
            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(putContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
            }
        }
    }
}
