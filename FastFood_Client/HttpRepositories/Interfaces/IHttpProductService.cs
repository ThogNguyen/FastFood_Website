using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;

namespace FastFood_Client.HttpRepositories.Interfaces   
{
    public interface IHttpProductService
    {
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task<ProductForView> GetProductByIdAsync(Guid id);
        Task CreateProductAsync(ProductForCreate productForCreate);
        Task CreateProduct(ProductForCreate productForCreate);
        Task UpdateProductAsync(Guid id, ProductForUpdate productForUpdate);
        Task<IEnumerable<ProductForView>> GetProductsByCategoryAsync(Guid categoryId);
        Task<IEnumerable<ProductForView>> GetProductsAsync(string? keyword, string? sortOption, Guid? categoryId);
    }
}
