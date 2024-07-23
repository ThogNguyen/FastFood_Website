using Services.Models.UserModel;
namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserForCreate userDto);
        Task<bool> UpdateUserAsync(UserForUpdate userDto, string id);
        Task<IEnumerable<UserForView>> GetAllUsersAsync();
        Task<UserForView> GetUserByIdAsync(string id);
    }
}
