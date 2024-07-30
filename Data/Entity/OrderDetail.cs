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
    [Table("OrderDetail")]
    public class OrderDetail : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        // unit price = price * quantity
        public decimal UnitPrice { get; set; }
        // Foreign key
        public Guid Order_Id { get; set; }
        [ForeignKey(nameof(Order_Id))]
        public Order? Order { get; set; }

        public Guid Product_Id { get; set; }
        [ForeignKey(nameof(Product_Id))]
        public Product? Product { get; set; }
    }
}
