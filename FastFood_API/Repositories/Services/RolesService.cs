using AutoMapper;
using Data.Models.AccountModels.Response;
using Data.Models.RoleModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreateRoleAsync(RoleForCreate roleForCreate)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleForCreate.Name!);
            if (roleExists)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Vai trò đã tồn tại."
                };
            }

            var role = _mapper.Map<IdentityRole>(roleForCreate);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return new BaseResponseMessage { IsSuccess = true };
            }

            return new BaseResponseMessage
            {
                IsSuccess = false,
                Errors = "Tạo role thất bại."
            };
        }

        public async Task<BaseResponseMessage> UpdateRoleAsync(string id, RoleForUpdate roleForUpdate)
        {
            // Tìm vai trò hiện tại theo ID
            var existingRole = await _roleManager.FindByIdAsync(id);
            if (existingRole == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Không tìm thấy vai trò."
                };
            }

            // Ánh xạ các thuộc tính từ RoleForUpdate đến vai trò hiện tại
            _mapper.Map(roleForUpdate, existingRole);

            // Cập nhật vai trò
            var result = await _roleManager.UpdateAsync(existingRole);

            if (result.Succeeded)
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
                Errors = "Cập nhật role thất bại."
            };
        }

        public async Task<BaseResponseMessage> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Không tìm thấy role."
                };
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return new BaseResponseMessage { IsSuccess = true };
            }

            return new BaseResponseMessage
            {
                IsSuccess = false,
                Errors = "Xóa role thất bại."
            };
        }

        public async Task<RoleForView> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return role != null ? _mapper.Map<RoleForView>(role) : null!;
        }

        public async Task<IEnumerable<RoleForView>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleForView>>(roles);
        }
    }
}
