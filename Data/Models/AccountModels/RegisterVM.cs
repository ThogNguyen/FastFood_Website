using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.AccountModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Họ và tên không được bỏ trống")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        // mẫu: 012 345 6789 | số 0 là mặc định phải có ở đầu + 9 số sau
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }
    }
}
