using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IProductsService
    {
        Task<BaseResponseMessage> CreateProductAsync(ProductForCreate productDto);
        Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id);
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task<ProductForView> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductForView>> GetProductsAsync(string? keyword, string? sortOption, Guid? categoryId);


    }
}
