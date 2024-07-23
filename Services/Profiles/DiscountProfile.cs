using AutoMapper;
using Domain.Entity;
using Services.Models.DiscountModel;
namespace Services.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<DiscountForCreate, Discount>();
            CreateMap<DiscountForUpdate, Discount>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Discount, DiscountForView>();
        }
    }
}
