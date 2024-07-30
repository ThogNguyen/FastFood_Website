using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.Extensions.Options;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpProductService : IHttpProductService
    {
        private readonly HttpClient _httpClient;

        public HttpProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<BaseResponseMessage> CreateProductAsync(ProductForCreate productDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductForView>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7241/api/ProductsApi/get-products");
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

        public Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
