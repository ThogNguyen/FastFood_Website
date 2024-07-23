using AutoMapper;
using Domain.Entity;
using Services.Models.OrderModel;
using Services.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<OrderForCreate, Order>();
            CreateMap<OrderForUpdate, Order>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Order, OrderForView>();
        }
    }
}
