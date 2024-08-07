using Data.Models.AccountModels.Response;
using Data.Models.OrderModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FastFoodWeb_Client.HttpRepositories.Services
{
    public class HttpOrderService : IHttpOrderService
    {
        private readonly HttpClient _httpClient;

        public HttpOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderAsync(OrderForCreate orderDto)
        {
            string data = JsonConvert.SerializeObject(orderDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44346/api/OrdersApi/cteate-order", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(responseContent);
            }
        }

        public async Task<IEnumerable<OrderForView>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44346/api/OrdersApi/get-orders");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderForView>>(content)!;
                return orders;
            }
            throw new ApplicationException(content);
        }

        public async Task<OrderForView> GetOrderByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44346/api/OrdersApi/get-order/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderForView>(content)!;
                return order;
            }
            throw new ApplicationException("Order not found.");
        }

        public async Task UpdateOrderAsync(OrderForUpdate orderDto, Guid id)
        {
            string data = JsonConvert.SerializeObject(orderDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/OrdersApi/update-order-status/{id}", content);
            var putContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(putContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
            }
        }

        public async Task UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id)
        {
            string data = JsonConvert.SerializeObject(shippingStatus); 
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:44346/api/OrdersApi/update-shipping-status/{id}", content);
            var putContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<BaseResponseMessage>(putContent);
                throw new ApplicationException(errorResponse?.Errors ?? "Đã xảy ra lỗi không xác định.");
            }
        }
    }
}
