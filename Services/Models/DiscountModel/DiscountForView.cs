using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.DiscountModel
{
    public class DiscountForView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal MaxDiscountValue { get; set; }
        public string? Description { get; set; }
        public bool IsPercentDiscount { get; set; } = false; // ko giam gia theo % mac dinh
        public int LimitQuantity { get; set; } // số lượng vé giảm giá
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
    }
}
