using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.OrderModels
{
    public class OrderForView
    {
        public Guid Id { get; set; }
        public string? User_Id { get; set; }    
        public Guid Coupon_Id { get; set; }
        public string? FullName { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Code { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTimeOffset? OrderDate { get; set; } = DateTimeOffset.Now;
        public string? OrderStatus { get; set; }
        public string? Note { get; set; }
        public string? ShippingStatus { get; set; }
        public string? DeliveryAddress { get; set; }
        public IList<OrderDetailForView> Items { get; set; } = new List<OrderDetailForView>();

    }

    public class OrderDetailForView
    {
        public Guid Product_Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        // tính số tiền giảm giá ở product , ví dụ price 99k, discount trong product 50k. thì UnitPrice là 44k
        public decimal? UnitPrice { get; set; }
    }
}
