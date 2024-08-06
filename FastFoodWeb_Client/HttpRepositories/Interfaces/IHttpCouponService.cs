using Data.Models.CouponModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpCouponService
    {
        Task<List<CouponForView>> GetAllCoupon(); // Sử dụng lớp CouponForView
        Task CreateCoupons(CouponForCreate model); // Sử dụng lớp CouponForCreate
        Task<CouponForView> GetCoupons(Guid id); // Sử dụng lớp CouponForView
        Task UpdateCoupons(Guid id, CouponForUpdate model); // Sử dụng lớp CouponForUpdate
        Task DeleteCoupons(Guid id); // Sử dụng lớp CouponForView
    }
}
