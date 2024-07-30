using Data.Enums;
using Data.Models.AccountModels.Response;
using Data.Models.OrderModels;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrdersService _orderService;
        public OrdersApiController(IOrdersService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các đơn hàng.
        /// </summary>
        /// <returns>Danh sách các đơn hàng.</returns>
        /// <response code="200">Trả về danh sách các đơn hàng.</response>
        [HttpGet("get-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Lấy thông tin của một đơn hàng theo ID.
        /// </summary>
        /// <param name="id">ID của đơn hàng cần lấy thông tin.</param>
        /// <returns>Thông tin sản phẩm.</returns>
        /// <response code="200">Lấy thông tin đơn hàng thành công.</response>
        /// <response code="404">Không tìm thấy đơn hàng với ID cung cấp.</response>
        [HttpGet("get-order/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id); 
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Tạo một đơn hàng mới.
        /// </summary>
        /// <param name="orderDto">Dữ liệu đơn hàng để tạo mới.</param>
        /// <returns>Kết quả của việc tạo đơn hàng.</returns>
        /// <response code="200">Đơn hàng được tạo thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPost("cteate-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] OrderForCreate orderDto)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState
                    .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                    .ToList();

                return BadRequest(new BaseListResponseMessage
                {
                    IsSuccess = false,
                    Errors = modelErrors
                });
            }
            var result = await _orderService.CreateOrderAsync(orderDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Cập nhật trạng thái vận chuyển của một đơn hàng theo ID.
        /// </summary>
        /// <param name="shippingStatus">Dữ liệu trạng thái vận chuyển để cập nhật.</param>
        /// <param name="id">ID của đơn hàng cần cập nhật trạng thái vận chuyển.</param>
        /// <returns>Kết quả của việc cập nhật trạng thái vận chuyển.</returns>
        /// <response code="200">Trạng thái vận chuyển đã được cập nhật thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-shipping-status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutShippingStatus([FromBody] OrderForUpdateShippingStatus shippingStatus, Guid id)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState
                    .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                    .ToList();

                return BadRequest(new BaseListResponseMessage
                {
                    IsSuccess = false,
                    Errors = modelErrors
                });
            }

            var result = await _orderService.UpdateOrderStatusShippingAsync(shippingStatus, id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Cập nhật thông tin của một đơn hàng theo ID.
        /// </summary>
        /// <param name="orderDto">Dữ liệu đơn hàng để cập nhật.</param>
        /// <param name="id">ID của đơn hàng cần cập nhật thông tin.</param>
        /// <returns>Kết quả của việc cập nhật thông tin đơn hàng.</returns>
        /// <response code="200">Thông tin đơn hàng đã được cập nhật thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-order-status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] OrderForUpdate orderDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState
                    .SelectMany(ms => ms.Value!.Errors.Select(e => e.ErrorMessage))
                    .ToList();

                return BadRequest(new BaseListResponseMessage
                {
                    IsSuccess = false,
                    Errors = modelErrors
                });
            }
            var result = await _orderService.UpdateOrderAsync(orderDto, id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
