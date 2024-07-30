using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.ProductModels.Base
{
    public class ProductBaseDTO
    {
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống.")]
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal? Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá giảm phải lớn hơn 0.")]
        public decimal? Discount { get; set; } = 0;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Mã loại không được bỏ trống.")]
        public Guid? Category_Id { get; set; }
    }
}
