using Data.Models.UserModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpUserService
    {
        Task<IEnumerable<UserForView>> GetAllUsersAsync();
        Task<UserForView> GetUserByIdAsync(string id);
        Task UpdateUserAsync(string id, UserForUpdate userDto);
    }
}
