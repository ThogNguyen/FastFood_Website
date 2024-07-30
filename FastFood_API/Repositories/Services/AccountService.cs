using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.AccountModels;
using Data.Models.AccountModels.Response;
using Data.Models.MailModels;
using FastFood_API.Helpers;
using FastFood_API.Helpers.MailServices;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FastFood_API.Repositories.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTHandle _jwtHandle;
        private readonly IMailService _mailService;

        public AccountService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, JWTHandle jwtHandle, IMailService mailService)
        {
            db = context;
            _mapper = mapper;
            _userManager = userManager;
            _jwtHandle = jwtHandle; 
            _mailService = mailService;
        }

        public async Task<LoginVMResponse> LoginAsync(LoginVM loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.Username!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginVM.Password!))
            {
                return new LoginVMResponse
                {
                    IsSuccess = false,
                    Error = "Tên đăng nhập hoặc mật khẩu không đúng"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtHandle.CreateToken(user, roles);

            return new LoginVMResponse
            {
                IsSuccess = true,
                Token = token
            };
        }
        public async Task<BaseResponseMessage> RegisterAsync(RegisterVM registerVM)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await db.Users
                .FirstOrDefaultAsync(u => u.Email == registerVM.Email);
            if (existingUser != null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Email đã được sử dụng."
                };
            }

            // Ánh xạ từ RegisterVM sang ApplicationUser
            ApplicationUser user = _mapper.Map<ApplicationUser>(registerVM);

            // Tạo người dùng mới và mã hóa mật khẩu
            var result = await _userManager.CreateAsync(user, registerVM.Password!);

            await _userManager.AddToRoleAsync(user, "Customer");

            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = null
            };
        }
        public async Task<BaseResponseMessage> ForgotPasswordAsync(ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Email không tồn tại."
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // api reset-password
            var callbackUrl = $"https://localhost:7241/api/Accounts/reset-password?email=" +
                                $"{user.Email}&token={tokenEncoded}";

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "XÁC NHẬN ĐỔI MẬT KHẨU",
                Body = $@"
                <html>
                <head>
                    <style>
                        /* CSS inline */
                        body, p, a {{
                            color: #000000; 
                            font-family: Arial, sans-serif;
                        }}
                    </style>
                </head>
                <body>
                    <p>Vui lòng nhấp vào đường dẫn để đổi mật khẩu mới: 
                        <a href='{callbackUrl}'>Xác nhận</a>
                    </p>
                </body>
                </html>"
            };

            await _mailService.SendEmailAsync(mailRequest);

            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Vui lòng kiểm tra Email của bạn."
            };
        }
        public async Task<BaseResponseMessage> ResetPasswordAsync(ResetPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Email không tồn tại."
                };
            }

            // Giải mã token
            var tokenDecoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token!));

            // Đặt lại mật khẩu
            var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, model.Password!);

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
                Errors = "Đổi mật khẩu thất bại"
            };
        }
    }
}
