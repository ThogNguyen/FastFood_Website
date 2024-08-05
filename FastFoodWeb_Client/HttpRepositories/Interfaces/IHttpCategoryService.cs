using Data.Models.CategoryModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpCategoryService
    {
        Task<List<CategoryForView>> GetAllCategory();
        Task CreateCategory(CategoryForCreate model);
        Task<CategoryForView> GetCategory(Guid id);
        Task UpdateCategory(Guid id, CategoryForUpdate model);
    }
}
