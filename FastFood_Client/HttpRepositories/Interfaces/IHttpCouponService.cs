using Data.Models.CouponModels;

public interface IHttpCouponService
{
    Task<List<CouponForView>> GetAllCoupon(); // Sử dụng lớp CouponForView
    Task CreateCoupons(CouponForCreate model); // Sử dụng lớp CouponForCreate
    Task<CouponForView> GetCoupons(Guid id); // Sử dụng lớp CouponForView
    Task UpdateCoupons(CouponForUpdate model); // Sử dụng lớp CouponForUpdate
    Task DeleteCoupons(Guid id); // Sử dụng lớp CouponForView
}
