using Data.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.CartModels
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductForView? Product { get; set; }
    }
}
