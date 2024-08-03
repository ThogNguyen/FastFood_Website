using Data.Models.AccountModels.Response;
using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

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
            var products = await _httpClient.GetAsync("https://localhost:7241/api/ProductsApi/get-products");
            if (products.IsSuccessStatusCode)
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

        public async Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7241/api/ProductsApi/get-product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<ProductForView>();
                return product;
            }
            throw new Exception("Lỗi khi lấy thông tin sản phẩm.");
        }

        public async Task<IEnumerable<ProductForView>> GetProductsByCategoryAsync(Guid categoryId)
        {
            Console.WriteLine($"GetProductsByCategoryAsync: categoryId = {categoryId}");
            var response = await _httpClient.GetAsync($"https://localhost:7241/api/ProductsApi/get-products-by-category/{categoryId}");
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

        public Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id)
        {
            throw new NotImplementedException();
        }

    }
}