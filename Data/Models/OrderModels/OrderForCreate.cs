using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.OrderModels
{
    public class OrderForCreate
    {
        public Guid Coupon_Id { get; set; }
        public string? User_Id { get; set; }
        public string? Note { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng không được bỏ trống")]
        public string? DeliveryAddress { get; set; }
        public IList<OrderDetailForCreate> Items { get; set; } = new List<OrderDetailForCreate>();
    }

    public class OrderDetailForCreate
    {
        public Guid Product_Id { get; set; }
        [Required(ErrorMessage = "Số lượng không được bỏ trống")]
        [Range(0,double.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 0")]
        public int Quantity { get; set; } = 1;
    }
}
