using Data.Models.UserRolesModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpUserRoleService
    {
        Task<IEnumerable<UserRoleForView>> GetAllUserRolesAsync();
        Task<UserRoleForView> GetUserRolesByUserIdAsync(string userId);
        Task UpdateUserRoleAsync(string userId, UserRoleForUpdate userRole);
    }
}
