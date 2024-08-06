using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller liên quan đến quản lý sản phẩm.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductsService _productService;
        public ProductsApiController(IProductsService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Lấy thông tin danh sách sản phẩm
        /// </summary>
        /// <returns>Danh sách sản phẩm.</returns>
        /// <response code="200">Trả về danh sách sản phẩm.</response>
        [HttpGet("get-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }


        /// <summary>
        /// Lấy thông tin của một sản phẩm theo ID.
        /// </summary>
        /// <param name="id">ID của sản phẩm cần lấy thông tin.</param>
        /// <returns>Thông tin sản phẩm.</returns>
        /// <response code="200">Lấy thông tin sản phẩm thành công.</response>
        /// <response code="404">Không tìm thấy sản phẩm với ID cung cấp.</response>
        [HttpGet("get-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Tạo một sản phẩm mới.
        /// </summary>
        /// <param name="productDto">Thông tin sản phẩm mới cần tạo.</param>
        /// <returns>Kết quả tạo sản phẩm.</returns>
        /// <response code="200">Tạo sản phẩm thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPost("create-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ProductForCreate productDto)
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

            var result = await _productService.CreateProductAsync(productDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Cập nhật thông tin của một sản phẩm.
        /// </summary>
        /// <param name="id">ID của sản phẩm cần cập nhật.</param>
        /// <param name="productDto">Thông tin cập nhật của sản phẩm.</param>
        /// <returns>Kết quả cập nhật sản phẩm.</returns>
        /// <response code="200">Cập nhật sản phẩm thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] ProductForUpdate productDto, Guid id)
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

            var result = await _productService.UpdateProductAsync(productDto, id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("search-products")]
        public async Task<IActionResult> SearchProducts(
            [FromQuery] string? keyword,
            [FromQuery] string? sortOption,
            [FromQuery] Guid? categoryId)
        {
            // Sử dụng GetProductsAsync thay vì GetAllProductsAsync
            var products = await _productService.GetProductsAsync(keyword, sortOption, categoryId);

            // Không cần lọc và sắp xếp lại ở đây vì GetProductsAsync đã xử lý

            return Ok(products);
        }
    }
}
