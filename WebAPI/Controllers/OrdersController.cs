using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.OrderModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var orders = await _orderService.GetOrderByIdAsync(id);
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderForCreate orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);
            return Ok(order);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OrderForUpdate orderDto, Guid id)
        {
            var order = await _orderService.UpdateOrderAsync(orderDto, id);
            return Ok(order);
        }
    }
}
