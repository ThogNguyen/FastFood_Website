using Data.Models.RoleModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpRoleService
    {
        Task CreateRoleAsync(RoleForCreate roleForCreate);
        Task UpdateRoleAsync(string id, RoleForUpdate roleForUpdate);
        Task DeleteRoleAsync(string id);
        Task<RoleForView> GetRoleByIdAsync(string id);
        Task<IEnumerable<RoleForView>> GetAllRolesAsync();
    }
}
