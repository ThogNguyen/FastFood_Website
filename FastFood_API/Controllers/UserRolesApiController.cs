using Azure;
using Data.Models.AccountModels.Response;
using Data.Models.UserRolesModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller liên quan đến quản lý vai trò của người dùng.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesApiController : ControllerBase
    {
        private readonly IUsersRoleService _usersRoleService;

        public UserRolesApiController(IUsersRoleService usersRoleService)
        {
            _usersRoleService = usersRoleService;
        }
        
        /// <summary> 
        /// Lấy danh sách tất cả các vai trò của người dùng. 
        /// </summary>
        /// <response code="200">Lấy danh sách vai trò của người dùng thành công.</response>
        /// <response code = "404" > Nếu không tìm thấy vai trò của người dùng nào trong hệ thống.</response>
        [HttpGet("get-userroles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserRoleForView>>> GetAllUserRoles()
        {
            var userRoles = await _usersRoleService.GetAll();
            return Ok(userRoles);
        }


        /// <summary>
        /// Lấy thông tin vai trò của người dùng theo ID người dùng.
        /// </summary>
        /// <param name="id">ID của người dùng.</param>
        /// <returns>Thông tin vai trò của người dùng.</returns>
        /// <response code="200">Lấy thông tin vai trò của người dùng thành công.</response>
        /// <response code="404">Nếu không tìm thấy người dùng với ID cung cấp.</response>
        /// 
        [HttpGet("get-userrole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserRoleForView>> GetUserRolesByUserId(string id)
        {
            var userRole = await _usersRoleService.GetUserRolesByUserId(id);
            if (userRole == null)
            {
                return NotFound(new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Người dùng không tồn tại."
                });
            }

            return Ok(userRole);
        }


        /// <summary>
        /// Cập nhật vai trò của người dùng.
        /// </summary>
        /// <param name="id">ID của người dùng.</param>
        /// <param name="userRole">Thông tin vai trò mới của người dùng.</param>
        /// <returns>Thông báo về việc cập nhật vai trò.</returns>
        /// <response code="200">Cập nhật vai trò của người dùng thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        /// 
        [HttpPut("update-userrole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResponseMessage>> UpdateUserRole(string id, [FromBody] UserRoleForUpdate userRole)
        {
            // Kiểm tra tính hợp lệ 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _usersRoleService.UpdateUserRole(id, userRole);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
