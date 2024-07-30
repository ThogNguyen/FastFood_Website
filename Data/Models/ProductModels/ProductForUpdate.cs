using Data.Models.ProductModels.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.ProductModels
{
    public class ProductForUpdate : ProductBaseDTO
    {
        public bool? IsActive { get; set; }
        public bool? IsCombo { get; set; }
    }
}
