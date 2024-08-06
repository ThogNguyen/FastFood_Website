using Data.Models.AccountModels.Response;
using Data.Models.OrderModels;

namespace FastFoodWeb_Client.HttpRepositories.Interfaces
{
    public interface IHttpOrderService
    {
        Task CreateOrderAsync(OrderForCreate orderDto);
        Task UpdateOrderAsync(OrderForUpdate orderDto, Guid id);
        Task UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id);
        Task<IEnumerable<OrderForView>> GetAllOrdersAsync();
        Task<OrderForView> GetOrderByIdAsync(Guid id);
    }
}
