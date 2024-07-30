using Data.Models.AccountModels.Response;
using Data.Models.UserModels;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller cho việc quản lý người dùng.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService _userService;
        public UsersApiController(IUsersService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Lấy danh sách tất cả các người dùng.
        /// </summary>
        /// <returns>Danh sách các người dùng.</returns>
        /// <response code="200">Trả về danh sách các người dùng.</response>
        [HttpGet("get-users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Lấy thông tin của một người dùng theo ID.
        /// </summary>
        /// <param name="id">ID của người dùng cần lấy thông tin.</param>
        /// <returns>Thông tin sản phẩm.</returns>
        /// <response code="200">Lấy thông tin người dùng thành công.</response>
        /// <response code="404">Không tìm thấy người dùng với ID cung cấp.</response>
        [HttpGet("get-user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetId(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin của một người dùng.
        /// </summary>
        /// <param name="id">ID của người dùng cần cập nhật.</param>
        /// <param name="userDto">Thông tin cập nhật của người dùng.</param>
        /// <returns>Kết quả cập nhật người dùng.</returns>
        /// <response code="200">Cập nhật người dùng thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] UserForUpdate userDto, string id)
        {
            var user = await _userService.UpdateUserAsync(userDto, id);
            return Ok(user);
        }
    }
}
