using AutoMapper;
using Domain.Entity;
using Services.Models.UserModel;
namespace Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<UserForCreate, User>();
            CreateMap<UserForUpdate, User>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<User, UserForView>();
        }
    }
}
