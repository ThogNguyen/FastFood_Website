using AutoMapper;
using Data.Entity;
using Data.Models.AccountModels;

namespace FastFood_API.Helpers.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterVM, ApplicationUser>();
        }
    }
}
