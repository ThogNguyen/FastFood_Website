using Domain.Entity;
using Services.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductForCreate productDto);
        Task<bool> UpdateProductAsync(ProductForUpdate productDto, Guid id);
        Task<IEnumerable<ProductForView>> GetAllProductsAsync();
        Task<ProductForView> GetProductByIdAsync(Guid id);
    }
}
