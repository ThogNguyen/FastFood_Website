using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        /*[HttpPost]*/
        /*public async Task CreateProductAsync([FromBody]ProductForCreate productForCreate)
        {
            string data = JsonConvert.SerializeObject(productForCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/ProductsApi/create-product", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }*/

        public async Task CreateProduct([FromBody] ProductForCreate productForCreate)
        {
            Console.WriteLine("Calling GetAllProductsAsync...");
            var products = await _httpClient.GetAsync("https://localhost:7241/api/ProductsApi/get-products");
            Console.WriteLine($"GetAllProductsAsync: Status code = {products.StatusCode}");

            if (products.IsSuccessStatusCode)
            string data = JsonConvert.SerializeObject(productForCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("https://localhost:44346/api/ProductsApi/create-product", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                var productsData = await products.Content.ReadFromJsonAsync<IEnumerable<ProductForView>>();
                if (productsData != null)
                {
                    // Sử dụng async/await để chờ cho mỗi task hoàn thành 
                    var productList = await Task.WhenAll(productsData.Select(async p =>
                    {
                        var category = await _httpClient.GetAsync($"https://localhost:7241/api/CategoriesApi/get-category/{p.Category_Id}");
                        if (category.IsSuccessStatusCode)
                        {
                            var categoryData = await category.Content.ReadFromJsonAsync<CategoryForView>();
                            return new ProductForView
                            {
                                Id = p.Id,
                                ProductName = p.ProductName,
                                Price = p.Price,
                                Discount = p.Discount,
                                Image = p.Image,
                                IsActive = p.IsActive,
                                IsCombo = p.IsCombo,
                                Description = p.Description,
                                Category_Id = p.Category_Id,
                                CategoryName = categoryData.Name,
                            };
                        }
                        return p;
                    }));

                    // Trả về danh sách ProductForView sau khi tất cả task hoàn thành
                    return productList;
                }
            }
            return Enumerable.Empty<ProductForView>();
        }

        public Task UpdateProductAsync(Guid id, ProductForUpdate productForUpdate)
        {
            throw new NotImplementedException();
        }

        /*public async Task<IEnumerable<ProductForView>> GetAllProductsAsync()
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
        }*/

        /*public async Task<ProductForView> GetProductByIdAsync(Guid id)
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
        }*/

        /*public async Task UpdateProductAsync(Guid id, ProductForUpdate productForUpdate)
        {
            string data = JsonConvert.SerializeObject(productForUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        public Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id)
        {
            throw new NotImplementedException();
        }

            return Enumerable.Empty<ProductForView>();
        }
    }
}
