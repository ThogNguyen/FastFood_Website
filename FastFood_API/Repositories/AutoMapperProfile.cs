using AutoMapper;
using Data.Entity;
using Data.Models.ProductModels;
namespace FastFood_API.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductForView>();
            // Các mapping khác...
        }
    }
}
