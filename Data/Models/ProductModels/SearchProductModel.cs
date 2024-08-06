using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.ProductModels
{
    public class SearchProductModel
    {
        [Required]
        public string SearchTerm { get; set; } = "";

        public string PriceRange { get; set; } = "";

        public string SortOption { get; set; } = "";
    }
}
