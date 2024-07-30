using Data.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Order")]
    public class Order : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? OrderDate { get; set; } = DateTimeOffset.Now;        
        [Column(TypeName = "nvarchar(50)")]
        public string? OrderStatus { get; set; } = "Pending";
        public string? Note { get; set; }
        public string? ShippingStatus { get; set; } = "InProgress";
        public string? DeliveryAddress { get; set; }


        // Foreign key
        public Guid Coupon_Id { get; set; }
        [ForeignKey(nameof(Coupon_Id))]
        public Coupon? Coupon { get; set; }
        public string? User_Id { get; set; }
        [ForeignKey(nameof(User_Id))]
        public ApplicationUser? ApplicationUser { get; set; }
        // việc này giúp lấy dữ liệu
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
