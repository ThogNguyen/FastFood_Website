using AutoMapper;
using Data.Entity;
using Data.Models.ProductModels;

namespace FastFood_API.Helpers.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<ProductForCreate, Product>();
            CreateMap<ProductForUpdate, Product>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Product, ProductForView>();
        }
    }
}
