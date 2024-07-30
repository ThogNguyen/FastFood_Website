using Data.Models.AccountModels.Response;
using Data.Models.CouponModels;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller cho việc quản lý mã giảm giá.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsApiController : ControllerBase
    {
        private readonly ICouponsService _couponService;
        public CouponsApiController(ICouponsService couponService)
        {
            _couponService = couponService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các mã giảm giá.
        /// </summary>
        /// <returns>Danh sách các mã giảm giá.</returns>
        /// <response code="200">Trả về danh sách các mã giảm giá.</response>
        [HttpGet("get-coupons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var discounts = await _couponService.GetAllDiscountsAsync();
            return Ok(discounts);
        }

        /// <summary>
        /// Lấy thông tin của một mã giảm giá theo ID.
        /// </summary>
        /// <param name="id">ID của mã giảm giá cần lấy thông tin.</param>
        /// <returns>Thông tin sản phẩm.</returns>
        /// <response code="200">Lấy thông tin mã giảm giá thành công.</response>
        /// <response code="404">Không tìm thấy mã giảm giá với ID cung cấp.</response>
        [HttpGet("get-coupon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                var coupon = await _couponService.GetDiscountByIdAsync(id);
                return Ok(coupon);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Tạo một mã giảm giá mới.
        /// </summary>
        /// <param name="couponDto">Dữ liệu mã giảm giá để tạo mới.</param>
        /// <returns>Kết quả của việc tạo mã giảm giá.</returns>
        /// <response code="200">Mã giảm giá được tạo thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPost("create-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CouponForCreate couponDto)
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

            var result = await _couponService.CreateDiscountAsync(couponDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        /// <summary>
        /// Cập nhật thông tin của một mã giảm giá.
        /// </summary>
        /// <param name="id">ID của mã giảm giá cần cập nhật.</param>
        /// <param name="couponDto">Thông tin cập nhật của mã giảm giá.</param>
        /// <returns>Kết quả cập nhật mã giảm giá.</returns>
        /// <response code="200">Cập nhật mã giảm giá thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] CouponForUpdate couponDto, Guid id)
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
            var result = await _couponService.UpdateDiscountAsync(couponDto, id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
