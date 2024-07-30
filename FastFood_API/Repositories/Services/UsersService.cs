using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.UserModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersService(ApplicationDbContext context, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        public async Task<IEnumerable<UserForView>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            IEnumerable<UserForView> view = _mapper.Map<IEnumerable<UserForView>>(users);
            return view;
        }

        public async Task<UserForView> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng này.");
            }
            UserForView view = _mapper.Map<UserForView>(user);

            return view;
        }

        public async Task<BaseResponseMessage> UpdateUserAsync(UserForUpdate userDto, string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            _mapper.Map(userDto, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Sửa thông tin người dùng thành công."
            };
        }
    }
}
