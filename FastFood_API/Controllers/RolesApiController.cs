using Data.Models.AccountModels.Response;
using Data.Models.RoleModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller liên quan đến quản lý vai trò người dùng.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesApiController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        /// <summary>
        /// Tạo một vai trò mới.
        /// </summary>
        /// <param name="roleForCreate">Thông tin vai trò mới cần tạo.</param>
        /// <returns>Kết quả tạo vai trò.</returns>
        /// <response code="200">Tạo vai trò thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPost("create-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] RoleForCreate roleForCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _rolesService.CreateRoleAsync(roleForCreate);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Cập nhật thông tin của một vai trò.
        /// </summary>
        /// <param name="id">ID của vai trò cần cập nhật.</param>
        /// <param name="roleForUpdate">Thông tin cập nhật của vai trò.</param>
        /// <returns>Kết quả cập nhật vai trò.</returns>
        /// <response code="200">Cập nhật vai trò thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpPut("update-role/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleForUpdate roleForUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _rolesService.UpdateRoleAsync(id, roleForUpdate);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Xóa một vai trò.
        /// </summary>
        /// <param name="id">ID của vai trò cần xóa.</param>
        /// <returns>Kết quả xóa vai trò.</returns>
        /// <response code="200">Xóa vai trò thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        [HttpDelete("delete-role/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _rolesService.DeleteRoleAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Lấy thông tin của một vai trò theo ID.
        /// </summary>
        /// <param name="id">ID của vai trò cần lấy thông tin.</param>
        /// <returns>Thông tin vai trò.</returns>
        /// <response code="200">Lấy thông tin vai trò thành công.</response>
        /// <response code="404">Không tìm thấy vai trò với ID cung cấp.</response>
        [HttpGet("get-role/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _rolesService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound(new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Role not found."
                });
            }

            return Ok(role);
        }

        /// <summary>
        /// Lấy danh sách tất cả các vai trò.
        /// </summary>
        /// <returns>Danh sách các vai trò.</returns>
        /// <response code="200">Lấy danh sách vai trò thành công.</response>
        [HttpGet("get-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _rolesService.GetAllRolesAsync();
            return Ok(roles);
        }
    }
}
