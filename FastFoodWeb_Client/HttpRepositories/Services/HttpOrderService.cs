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

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent)
                    ?? throw new ApplicationException("Deserialization failed.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Error: {response.StatusCode}, {errorMessage}");
        }

        public Task<BaseResponseMessage> CreateOrderAsync(OrderForCreate orderDto)
        {
            var data = JsonConvert.SerializeObject(orderDto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44346/api/OrdersApi/create-order") { Content = content };
            return SendRequestAsync<BaseResponseMessage>(requestMessage);
        }

        public Task<BaseResponseMessage> UpdateOrderAsync(OrderForUpdate orderDto, Guid id)
        {
            var data = JsonConvert.SerializeObject(orderDto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:44346/api/OrdersApi/update-order/{id}") { Content = content };
            return SendRequestAsync<BaseResponseMessage>(requestMessage);
        }

        public Task<BaseResponseMessage> UpdateOrderStatusShippingAsync(OrderForUpdateShippingStatus shippingStatus, Guid id)
        {
            var data = JsonConvert.SerializeObject(shippingStatus);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:44346/api/OrdersApi/update-shipping-status/{id}") { Content = content };
            return SendRequestAsync<BaseResponseMessage>(requestMessage);
        }

        public Task<IEnumerable<OrderForView>> GetAllOrdersAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44346/api/OrdersApi/get-orders");
            return SendRequestAsync<IEnumerable<OrderForView>>(requestMessage);
        }

        public Task<OrderForView> GetOrderByIdAsync(Guid id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44346/api/OrdersApi/get-order/{id}");
            return SendRequestAsync<OrderForView>(requestMessage);
        }
    }
}
