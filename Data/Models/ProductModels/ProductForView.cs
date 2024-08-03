using Data.Models.ProductModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.ProductModels
{
    public class ProductForView : ProductBaseDTO
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public int Quantity { get; set; } = 1; // Mặc định là 1
    }
}
