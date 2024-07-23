using Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Order : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey(nameof(DiscountId))]
        public Guid DiscountId { get; set; }
        public Discount? discount { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public decimal OrderTotal { get; set; }
        [Required]
        public decimal DiscountTotal { get; set; }
        // đây là discount tổng từ discount sản phẩm + - discount bảng
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string OrderStatus { get; set; } = "Pending";
        // nếu chưa có làm gì cả thì mặc định luôn nằm trong hàng đợi :D
        [Required]
        public decimal VAT { get; set; }
        public string? Note { get; set; }
        // cho khách ghi yêu cầu với món ăn bla bla
        [Required]
        public DateTime ShippingDate { get; set; } = DateTime.Now;
        [Required]
        public string ShippingStatus { get; set; } = "Pending";
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        [ForeignKey(nameof(PaymentId))]
        public Guid PaymentId { get; set; }
        public Payment? payment { get; set; }
    }
}
