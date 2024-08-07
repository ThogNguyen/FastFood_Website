using AutoMapper;
using Data.Entity;
using Data.Models.OrderModels;

namespace FastFood_API.Helpers.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<OrderForCreate, Order>()
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.OrderDetails, y => y.Ignore());

            CreateMap<OrderDetailForCreate, OrderDetail>()
                //.ForMember(x => x.Product.Price - x.Product.Discount, y => y.MapFrom(z => z.UnitPrice))
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Order, y => y.Ignore());

            CreateMap<OrderForUpdate, Order>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Order, OrderForView>()
                .ForMember(x => x.Code, y => y.MapFrom(z => z.Coupon!.Code))
                .ForMember(x => x.DiscountPrice, y => y.MapFrom(z => z.Coupon!.DiscountPrice))
                .ForMember(x => x.Items, y => y.MapFrom(z => z.OrderDetails))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.ApplicationUser!.FullName));


            CreateMap<OrderDetail, OrderDetailForView>()
                .ForMember(x => x.Product_Id, y => y.MapFrom(z => z.Product_Id))
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product!.ProductName));
            // bây giờ muốn có ProductName lên UI (OrderDetailForView) cho nên phải chọc vô DB tức OrderDetail, và trong OrderDetail ta có 
            // public Product? Product { get; set; }
            // cho nên từ đó ta chọc vô Product lấy name của nó để chuyển thành ProductName cho UI
            // Nguồn from OrderDetail, Đích đến from OrderDetailForView

            CreateMap<OrderForUpdateShippingStatus, Order>()
                .ForMember(dest => dest.ShippingStatus, opt => opt.MapFrom(src => src.ShippingStatus));
        }
    }
}
