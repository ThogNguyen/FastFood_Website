using AutoMapper;
using Domain.Entity;
using Services.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<ProductForCreate, Product>();
            CreateMap<ProductForUpdate, Product>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Product, ProductForView>();
        }
    }
}
