using AutoMapper;
using Data.Entity;
using Data.Models.CouponModels;

namespace FastFood_API.Helpers.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<CouponForCreate, Coupon>();
            CreateMap<CouponForUpdate, Coupon>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Coupon, CouponForView>();
        }
    }
}
