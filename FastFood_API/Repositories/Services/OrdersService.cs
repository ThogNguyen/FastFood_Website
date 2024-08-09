using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Enums;
using Data.Models.AccountModels.Response;
using Data.Models.OrderModels;
using FastFood_API.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services    
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrdersService(ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<BaseResponseMessage> CreateOrderAsync(OrderForCreate orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTimeOffset.UtcNow;
            //đang map từ giao diện UI vào db.
            order.OrderDetails = _mapper.Map<ICollection<OrderDetail>>(orderDto.Items);

            decimal totalOrderDetailPrice = 0; // lấy tổng số tiền trong order
            foreach (var item in order.OrderDetails)
            {
                var getProduct = _context.Products.SingleOrDefault(x => x.Id == item.Product_Id);
                item.Price = getProduct!.Price - getProduct.Discount;
                item.UnitPrice = item.Price * item.Quantity;
                totalOrderDetailPrice += item.UnitPrice;
            }

            decimal totalOrderPrice = 0; // sau khi giảm giá
            var discount = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == order.Coupon_Id);
            if (discount != null)
            {
                if (!discount.IsActive)
                {
                    return new BaseResponseMessage
                    {
                        IsSuccess = false,
                        Errors = "Mã giảm giá không còn hiệu lực."
                    };
                }

                if (discount.IsPercentDiscount)
                {
                    totalOrderPrice = totalOrderDetailPrice - 
                            (totalOrderDetailPrice * discount.PercentDiscount / 100);
                    if (totalOrderPrice >= discount.MaxDiscountValue)
                    {
                        totalOrderPrice = totalOrderDetailPrice - discount.MaxDiscountValue;
                    }
                }
                else
                {
                    totalOrderPrice = totalOrderDetailPrice - discount.DiscountPrice;
                }
            }
            order.TotalAmount = totalOrderPrice;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Tạo order thành công."
            };
        }

        public async Task<IEnumerable<OrderForView>> GetAllOrdersAsync()
        {
            var getOrders = await _context.Orders
                    .Include(x => x.Coupon)
                    .Include(x => x.OrderDetails!).ThenInclude(y => y.Product)
                    .Include(x => x.ApplicationUser)
                    .ToListAsync();

            IList<OrderForView> orderItems = 
                _mapper.Map<IEnumerable<OrderForView>>(getOrders).ToList();
            return orderItems;
        }

        public async Task<OrderForView> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                throw new Exception("Không tìm thấy sản phẩm này");
            }
            OrderForView view = _mapper.Map<OrderForView>(order);

            return view;
        }

        public async Task<BaseResponseMessage> UpdateOrderAsync(OrderForUpdate orderDto, Guid id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            
            // Kiểm tra trạng thái của đơn hàng
            if (order.OrderStatus == OrderStatus.Paid.ToString())
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Đơn hàng đã được thanh toán và không thể cập nhật."
                };
            }

            _mapper.Map(orderDto, order);
            if (order!.OrderStatus != OrderStatus.Pending.ToString() ||
                order.OrderStatus != OrderStatus.UnPaid.ToString())
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new BaseResponseMessage
                {
                    IsSuccess = true,
                    Errors = "Cập nhật order thành công."
                };
            }
            return new BaseResponseMessage
            {
                IsSuccess = false,
                Errors = "Cập nhật order thất bại."
            };
        }

        public async Task<BaseResponseMessage> UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            _mapper.Map(shippingStatus, order);
            if (order!.OrderStatus == OrderStatus.Paid.ToString())
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new BaseResponseMessage
                {
                    IsSuccess = true,
                    Errors = "Cập nhật trạng thái giao hàng thành công."
                };
            }
            return new BaseResponseMessage
            {
                IsSuccess = false,
                Errors = "Đơn hàng đang được vận chuyển."
            };
        }
    }
}
