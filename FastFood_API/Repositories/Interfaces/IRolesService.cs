using Data.Models.AccountModels.Response;
using Data.Models.RoleModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleForView>> GetAllRolesAsync();
        Task<RoleForView> GetRoleByIdAsync(string id);
        Task<BaseResponseMessage> CreateRoleAsync(RoleForCreate roleForCreate);
        Task<BaseResponseMessage> UpdateRoleAsync(string id, RoleForUpdate roleForUpdate);
        Task<BaseResponseMessage> DeleteRoleAsync(string id);
    }
}
