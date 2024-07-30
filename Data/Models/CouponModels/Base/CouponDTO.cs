using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.CouponModels.Base
{
    public class CouponDTO
    {
        [Required(ErrorMessage = "Mã giảm giá không được bỏ trống.")]
        public string? Code { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá giảm phải lớn hơn 0.")]
        public decimal DiscountPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá giảm tối đa phải lớn hơn 0.")]
        public decimal MaxDiscountValue { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int PercentDiscount { get; set; }
        public bool IsPercentDiscount { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}
