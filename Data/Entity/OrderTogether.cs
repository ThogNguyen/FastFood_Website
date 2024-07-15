using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class OrderTogether
    {
        [Key]
        public int Id { get; set; }
        public string? MainOrderName { get; set; }

        // Foreign key
        public int Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public Order Order { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
