using Data.Models.AccountModels.Response;
using Data.Models.OrderModels;

namespace FastFood_API.Repositories.Interfaces
{
    public interface IOrdersService
    {
        Task<BaseResponseMessage> CreateOrderAsync(OrderForCreate orderDto);
        Task<BaseResponseMessage> UpdateOrderAsync(OrderForUpdate orderDto, Guid id);
        Task<BaseResponseMessage> UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id);
        Task<IEnumerable<OrderForView>> GetAllOrdersAsync();
        Task<OrderForView> GetOrderByIdAsync(Guid id);
    }
}
