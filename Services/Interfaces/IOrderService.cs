using Services.Models.OrderModel;
using Services.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderForCreate orderDto);
        Task<bool> UpdateOrderAsync(OrderForUpdate orderDto, Guid id);
        Task<IEnumerable<OrderForView>> GetAllOrdersAsync();
        Task<OrderForView> GetOrderByIdAsync(Guid id);
    }
}
