using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FastFoodWeb_Client.HttpRepositories.Services
{
    public class HttpProductService : IHttpProductService
    {
        private readonly HttpClient _httpClient;

        public HttpProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateProduct(ProductForCreate productForCreate)
        {
            string data = JsonConvert.SerializeObject(productForCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/ProductsApi/create-product", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(postContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
            }
        }

        public async Task<IEnumerable<ProductForView>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/ProductsApi/get-products");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductForView>>();
                if (products != null)
                {
                    return products;
                }
            }
            return Enumerable.Empty<ProductForView>();
        }

        public async Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/ProductsApi/get-product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<ProductForView>();
                if (product != null)
                {
                    return product;
                }
            }
            throw new ApplicationException($"Product with ID {id} not found. Status code: {response.StatusCode}");
        }

        public async Task UpdateProductAsync(Guid id, ProductForUpdate productForUpdate)
        {
            string data = JsonConvert.SerializeObject(productForUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/ProductsApi/update-product/{id}", content);
            var putContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(putContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
            }
        }

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/ImageUpload/upload-product-image", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                // Giả sử postContent chứa đường dẫn hình ảnh tương đối
                var imgUrl = $"https://localhost:44346/{postContent}";
                return imgUrl;
            }
        }
    }
}
