using Data.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.OrderModels
{
    public class OrderForUpdate
    {
        [Required(ErrorMessage = "Trạng thái đơn hàng không được bỏ trống")]
        public OrderStatus OrderStatus { get; set; }
        // public string? Note { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        // [Required(ErrorMessage = "Địa chỉ giao hàng không được bỏ trống")]
        //public string? DeliveryAddress { get; set; }
    }

    public class OrderDetailForUpdate
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        // tính số tiền giảm giá ở product , ví dụ price 99k, discount trong product 50k.
        // thì UnitPrice là 44k
        // unit price = price * quantity
        public decimal Price { get; set; } // giá sau khi giảm
        public decimal UnitPrice { get; set; } // tổng tiền của số lượng sản phẩm đó
    }
}
