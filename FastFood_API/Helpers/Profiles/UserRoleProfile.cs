using AutoMapper;
using Data.Models.RoleModels;
using Data.Models.UserRolesModels;
using Microsoft.AspNetCore.Identity;

namespace FastFood_API.Helpers.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            // Chuyển từ giao diện hiển thị tới DB
            CreateMap<UserRoleForUpdate, IdentityUserRole<string>>();

            // Hiển thị từ DB ra giao diện
            CreateMap<IdentityUserRole<string>, UserRoleForView>();
        }
    }
}
