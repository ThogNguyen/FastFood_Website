using AutoMapper;
using Data.Entity;
using Data.Models.CategoryModels;

namespace FastFood_API.Helpers.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryForView>();
            CreateMap<CategoryForUpdate, Category>();
            CreateMap<CategoryForCreate, Category>();
        }
    }
}
