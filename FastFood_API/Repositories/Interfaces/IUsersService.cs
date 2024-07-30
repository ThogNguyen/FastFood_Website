using Data.Models.AccountModels.Response;
using Data.Models.UserModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IUsersService
    {
        Task<BaseResponseMessage> UpdateUserAsync(UserForUpdate userDto, string id);
        Task<IEnumerable<UserForView>> GetAllUsersAsync();
        Task<UserForView> GetUserByIdAsync(string id);
    }
}
