using AutoMapper;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.UserRolesModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FastFood_API.Repositories.Services
{
    public class UsersRoleService : IUsersRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersRoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserRoleForView>> GetAll()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new List<UserRoleForView>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        var userRole = new IdentityUserRole<string>
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };
                        var userRoleForView = _mapper.Map<UserRoleForView>(userRole);
                        userRoleForView.UserName = user.UserName;
                        userRoleForView.RoleName = role.Name;
                        userRoles.Add(userRoleForView);
                    }
                }
            }

            return userRoles;
        }

        public async Task<UserRoleForView> GetUserRolesByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var userRoleForView = new UserRoleForView
            {
                UserId = user.Id,
                UserName = user.UserName,
                RoleId = roles.FirstOrDefault(),
                RoleName = roles.FirstOrDefault()
            };

            return userRoleForView;
        }

        public async Task<BaseResponseMessage> UpdateUserRole(string id, UserRoleForUpdate userRole)
        {
            // Tìm người dùng theo ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Người dùng không tồn tại."
                };
            }

            // Kiểm tra xem vai trò mới có tồn tại không
            var roleExists = await _roleManager.RoleExistsAsync(userRole.RoleName);
            if (!roleExists)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Tên vai trò không tồn tại."
                };
            }

            // Xoá vai trò hiện tại của người dùng
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Không thể xoá vai trò của người dùng."
                };
            }

            // Thêm vai trò mới
            var addRoleResult = await _userManager.AddToRoleAsync(user, userRole.RoleName);
            if (addRoleResult.Succeeded)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = true,
                    Errors = null
                };
            }
            return new BaseResponseMessage
            {
                IsSuccess = false,
                Errors = "Không thể thay đổi vai trò người dùng."
            };
        }
    }
}
