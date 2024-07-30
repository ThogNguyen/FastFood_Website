using Data.Models.AccountModels.Response;
using Data.Models.CategoryModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryForView>> GetAllCategoriesAsync();
        Task<BaseResponseMessage> CreateCategoryAsync(CategoryForCreate categoryDto);
        Task<CategoryForView> GetCategoryByIdAsync(Guid id);
        Task<BaseResponseMessage> UpdateCategoryAsync(CategoryForUpdate categoryDto, Guid id);
    }
}
