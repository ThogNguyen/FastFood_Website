using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.AccountModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string? Password { get; set; }
    }
}
