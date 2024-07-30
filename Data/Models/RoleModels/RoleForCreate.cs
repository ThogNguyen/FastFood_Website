using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.RoleModels
{
    public class RoleForCreate
    {
        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        public string? Name { get; set; }
    }
}
