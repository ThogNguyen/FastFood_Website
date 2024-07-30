using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.AccountModels.Response
{
    public class BaseResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
    }
}
