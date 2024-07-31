using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;

namespace FastFood_Client.HttpRepositories.Interfaces   
{
    public interface IHttpProductService
    {
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task CreateProductAsync(ProductForCreate productForCreate);
    }
}
