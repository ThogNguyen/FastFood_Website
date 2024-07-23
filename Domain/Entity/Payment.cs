using Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Payment : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PaymentStatus { get; set; } = "Pending";
    }
}
