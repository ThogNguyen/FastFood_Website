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
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderTime { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? DeliveryAddress { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? PhoneNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Status { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentType { get; set; }
        public int TotalAmount { get; set; }
        public int TotalDiscount { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? DiscountCode { get; set; }

        // khóa ngoại
        [ForeignKey("Coupon_Id")]
        public int? Coupon_Id { get; set; }
        public Coupon? Coupon { get; set; }
        public ICollection<OrderTogether> OrderTogethers { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
