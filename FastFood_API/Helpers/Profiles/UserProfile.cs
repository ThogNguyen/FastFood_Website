using AutoMapper;
using Data.Entity;
using Data.Models.RoleModels;
using Data.Models.UserModels;
using Microsoft.AspNetCore.Identity;

namespace FastFood_API.Helpers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForUpdate, ApplicationUser>();
            CreateMap<ApplicationUser, UserForView>();
        }
    }
}
