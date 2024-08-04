using Data.Models.OrderModels;
using Data.Models.AccountModels.Response;

namespace FastFood_Client.HttpRepositories.Interfaces
{
    public interface IHttpOrderService
    {
        Task<BaseResponseMessage> CreateOrderAsync(OrderForCreate orderDto);
        Task<BaseResponseMessage> UpdateOrderAsync(OrderForUpdate orderDto, Guid id);
        Task<BaseResponseMessage> UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id);
        Task<IEnumerable<OrderForView>> GetAllOrdersAsync();
        Task<OrderForView> GetOrderByIdAsync(Guid id);
    }
}
