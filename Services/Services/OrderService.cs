using AutoMapper;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.OrderModel;
namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateOrderAsync(OrderForCreate orderDto)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderDto);
                await _context.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderForView>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                IEnumerable<OrderForView> view = _mapper.Map<IEnumerable<OrderForView>>(orders);
                return view;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<OrderForView> GetOrderByIdAsync(Guid id)
        {
            try
            {
                var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
                OrderForView view = _mapper.Map<OrderForView>(order);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception("Don't have this Order");
            }
        }

        public async Task<bool> UpdateOrderAsync(OrderForUpdate orderDto, Guid id)
        {
            try
            {
                var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
                _mapper.Map(orderDto, order);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
