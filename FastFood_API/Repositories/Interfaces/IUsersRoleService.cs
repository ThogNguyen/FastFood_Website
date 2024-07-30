using Data.Models.AccountModels.Response;
using Data.Models.UserRolesModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IUsersRoleService
    {
        Task<IEnumerable<UserRoleForView>> GetAll();
        Task<BaseResponseMessage> UpdateUserRole(string id, UserRoleForUpdate userRole);
        Task<UserRoleForView> GetUserRolesByUserId(string id);
    }
}
