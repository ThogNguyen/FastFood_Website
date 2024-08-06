using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.CartModels
{
    public class CartView
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
