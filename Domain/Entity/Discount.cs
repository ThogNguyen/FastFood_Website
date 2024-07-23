using Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Discount : EntityAuditBase
    {
        // discount ở đây sẽ theo giá, theo % và thêm 1 bảng giảm tới mức độ như thế nào
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal DiscountPrice {  get; set; }
        [Required]
        public decimal MaxDiscountValue { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool IsPercentDiscount {  get; set; } = false; // ko giam gia theo % mac dinh
        [Required]
        public int LimitQuantity { get; set; } // số lượng vé giảm giá
        [Required]
        public bool IsLimit {  get; set; } = false ; // bth giam ko co gioi han 
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; }
    }
}
