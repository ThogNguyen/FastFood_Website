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
    [Table("Product")]
    public class Product : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
            
        public decimal Discount { get; set; }
        [Column(TypeName = "nvarchar(400)")]
        public string? Image { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsCombo { get; set; } = false;
        [Column(TypeName = "nvarchar(400)")]
        public string? Description { get; set; }

        // Foreign Key
        public Guid Category_Id { get; set; }
        [ForeignKey(nameof(Category_Id))]
        public Category? Category { get; set; }


        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
