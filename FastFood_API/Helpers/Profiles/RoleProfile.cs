using AutoMapper;
using Data.Entity;
using Data.Models.RoleModels;
using Microsoft.AspNetCore.Identity;

namespace FastFood_API.Helpers.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<RoleForCreate, IdentityRole>();
            CreateMap<RoleForUpdate, IdentityRole>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<IdentityRole, RoleForView>();
        }
    }
}
