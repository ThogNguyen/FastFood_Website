using Data.Models.CategoryModels;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpCategoryService
    {
        Task<IEnumerable<CategoryForView>> GetCategoriesAsync();
    }
}
