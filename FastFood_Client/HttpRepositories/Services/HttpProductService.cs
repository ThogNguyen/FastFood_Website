using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FastFood_Client.HttpRepositories.Services
{
    public class HttpProductService : IHttpProductService
    {
        private readonly HttpClient _httpClient;

        public HttpProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(ProductForCreate productForCreate)
        {
            string data = JsonConvert.SerializeObject(productForCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/ProductsApi/create-product", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
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
    }
}
