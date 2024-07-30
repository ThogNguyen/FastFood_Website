using Data.Models.CouponModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.CouponModels
{
    public class CouponForView : CouponDTO
    {
        public Guid Id { get; set; }
    }
}
