using Data.Models.AccountModels.Response;
using Data.Models.CategoryModels;
using Data.Models.RoleModels;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller cho việc quản lý các loại sản phẩm.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các loại sản phẩm.
        /// </summary>
        /// <returns>Danh sách các loại sản phẩm.</returns>
        /// <response code="200">Trả về danh sách các loại sản phẩm.</response>
        [HttpGet("get-categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Tạo một loại sản phẩm mới.
        /// </summary>
        /// <param name="categoryDto">Dữ liệu loại sản phẩm để tạo mới.</param>
        /// <returns>Kết quả của việc tạo loại sản phẩm.</returns>
        /// <response code="200">Loại sản phẩm được tạo thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPost("create-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CategoryForCreate categoryDto)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }

            var result = await _categoryService.CreateCategoryAsync(categoryDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Lấy thông tin của một loại sản phẩm theo ID.
        /// </summary>
        /// <param name="id">ID của loại sản phẩm cần lấy thông tin.</param>
        /// <returns>Thông tin sản phẩm.</returns>
        /// <response code="200">Lấy thông tin loại sản phẩm thành công.</response>
        /// <response code="404">Không tìm thấy loại sản phẩm với ID cung cấp.</response>
        [HttpGet("get-category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một loại sản phẩm.
        /// </summary>
        /// <param name="id">ID của loại sản phẩm cần cập nhật.</param>
        /// <param name="categoryDto">Thông tin cập nhật của loại sản phẩm.</param>
        /// <returns>Kết quả cập nhật loại sản phẩm.</returns>
        /// <response code="200">Cập nhật loại sản phẩm thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("put-category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] CategoryForUpdate categoryDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.UpdateCategoryAsync(categoryDto, id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
