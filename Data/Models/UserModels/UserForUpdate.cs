using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.UserModels
{
    public class UserForUpdate
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }
    }
}
