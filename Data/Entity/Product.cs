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
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? ProductName { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "nvarchar(400)")]
        public string? Image { get; set; }
        [Column(TypeName = "nvarchar(400)")]
        public string? Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCombo { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
