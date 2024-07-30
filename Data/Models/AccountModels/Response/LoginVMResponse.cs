using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.AccountModels.Response
{
    public class LoginVMResponse
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}
