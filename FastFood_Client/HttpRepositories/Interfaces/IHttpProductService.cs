using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;

namespace FastFood_Client.HttpRepositories.Interfaces   
{
    public interface IHttpProductService
    {
        Task<BaseResponseMessage> CreateProductAsync(ProductForCreate productDto);
        Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id);
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task<ProductForView> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductForView>> GetProductsByCategoryAsync(Guid categoryId);
    }
}

