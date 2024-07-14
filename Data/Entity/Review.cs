using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerName { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Comment { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("User_Id")]
        [Column(TypeName = "nvarchar(450)")]
        public string? User_Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
