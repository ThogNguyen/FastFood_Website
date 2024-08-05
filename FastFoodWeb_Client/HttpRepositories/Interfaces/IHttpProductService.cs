using Data.Models.ProductModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpProductService
    {
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task<ProductForView> GetProductByIdAsync(Guid id);
        Task CreateProduct(ProductForCreate productForCreate);
        Task UpdateProductAsync(Guid id, ProductForUpdate productForUpdate);
    }
}
