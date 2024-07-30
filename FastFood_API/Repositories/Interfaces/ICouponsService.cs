using Data.Models.AccountModels.Response;
using Data.Models.CouponModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface ICouponsService
    {
        Task<BaseResponseMessage> CreateDiscountAsync(CouponForCreate discountDto);
        Task<BaseResponseMessage> UpdateDiscountAsync(CouponForUpdate discountDto, Guid id);
        Task<IEnumerable<CouponForView>> GetAllDiscountsAsync();
        Task<CouponForView> GetDiscountByIdAsync(Guid id);
    }
}
