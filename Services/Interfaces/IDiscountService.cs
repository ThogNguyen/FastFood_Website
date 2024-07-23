using Services.Models.DiscountModel;
using Services.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDiscountService
    {
        Task<bool> CreateDiscountAsync(DiscountForCreate discountDto);
        Task<bool> UpdateDiscountAsync(DiscountForUpdate discountDto, Guid id);
        Task<IEnumerable<DiscountForView>> GetAllDiscountsAsync();
        Task<DiscountForView> GetDiscountByIdAsync(Guid id);
    }
}
