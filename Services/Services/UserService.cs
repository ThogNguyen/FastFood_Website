using AutoMapper;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.UserModel;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserAsync(UserForCreate userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserForView>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                IEnumerable<UserForView> view = _mapper.Map<IEnumerable<UserForView>>(users);
                return view;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserForView> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
                UserForView view = _mapper.Map<UserForView>(user);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception("Don't have this User");
            }
        }

        public async Task<bool> UpdateUserAsync(UserForUpdate userDto, string id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
                _mapper.Map(userDto, user);
                _context.Users.Update(user); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
