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
    [Table("Coupon")]
    public class Coupon : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? Code { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal MaxDiscountValue { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true; // mặc định là active
        public int PercentDiscount { get; set; }
        public bool IsPercentDiscount { get; set; } = false;  // ko giam gia theo % mac dinh
        public DateTimeOffset? StartDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? EndDate { get; set; } = DateTimeOffset.Now;
        public ICollection<Order>? Orders { get; set; }
    }
}
