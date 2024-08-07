using Data.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.OrderModels
{
    public class OrderForUpdateShippingStatus
    {
        [Required(ErrorMessage = "Trạng thái giao hàng không được bỏ trống")]
        public ShippingStatus ShippingStatus { get; set; }
    }
}
