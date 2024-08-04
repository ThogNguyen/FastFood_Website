using Data.Models.UserModels;
using static FastFood_Client.Pages.Admin.Test1;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpUserService
    {
        Task<IEnumerable<UserForView>> GetAllUsersAsync();
        Task<UserForView> GetUserByIdAsync(string id);
        Task UpdateUserAsync(string id, UserForUpdate userDto);
    }
}
