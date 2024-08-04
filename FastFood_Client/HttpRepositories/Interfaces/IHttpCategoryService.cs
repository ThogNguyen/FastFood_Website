using Data.Models.CategoryModels;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpCategoryService
    {
        Task<List<CategoryForView>> GetAllCategory(); // Sử dụng lớp CategoryForView
        Task<List<CategoryForView>> GetCategoriesAsync(); 
        Task CreateCategory(CategoryForCreate model); // Sử dụng lớp CategoryForCreate
        Task<CategoryForView> GetCategory(Guid id); // Sử dụng lớp CategoryForView
        Task UpdateCategory(CategoryForUpdate model); // Sử dụng lớp CategoryForUpdate
    }
}
