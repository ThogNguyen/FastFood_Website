using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.CategoryModels
{
    public class CategoryForUpdate
    {
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string? Name { get; set; }
    }
}
