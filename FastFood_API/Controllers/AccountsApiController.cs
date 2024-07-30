using AutoMapper;
using AutoMapper.Internal;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.AccountModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using FastFood_API.Repositories.Interfaces;

namespace FastFood_API.Controllers
{
    /// <summary>
    /// Controller liên quan đến tài khoản người dùng.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Đăng ký người dùng mới.
        /// </summary>
        /// <param name="registerVM">Thông tin đăng ký của người dùng.</param>
        /// <returns>
        /// Trả về kết quả đăng ký.
        /// </returns>
        /// <response code="200">Đăng ký thành công.</response>
        /// <response code="400">Email đã được sử dụng hoặc thông tin không hợp lệ.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterVM registerVM)
        {
            // Kiểm tra tính hợp lệ 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Gọi phương thức RegisterAsync để đăng ký người dùng
            var result = await _accountService.RegisterAsync(registerVM);

            // Trả về kết quả
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Đăng nhập bằng cách xác thực thông tin đăng nhập và trả về JWT token nếu thành công.
        /// </summary>
        /// <param name="loginVM">Thông tin đăng nhập của người dùng.</param>
        /// <returns>
        /// Trả về kết quả đăng nhập.
        /// </returns>
        /// <response code="200">Đăng nhập thành công.</response>
        /// <response code="401">Tên đăng nhập hoặc mật khẩu không đúng.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginUser([FromBody] LoginVM loginVM)
        {
            // Kiểm tra tính hợp lệ 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gọi phương thức LoginAsync để đăng ký người dùng
            var result = await _accountService.LoginAsync(loginVM);

            // Trả về kết quả
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }

        /// <summary>
        /// Gửi liên kết đặt lại mật khẩu đến email của người dùng.
        /// </summary>
        /// <param name="model">Thông tin email của người dùng.</param>
        /// <returns>
        /// Trả về kết quả yêu cầu đặt lại mật khẩu.
        /// </returns>
        /// <response code="200">Kiểm tra email của bạn để đặt lại mật khẩu.</response>
        /// <response code="400">Email không tồn tại hoặc thông tin không hợp lệ.</response>
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.ForgotPasswordAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Đổi mật khẩu mới cho tài khoản người dùng.
        /// </summary>
        /// <param name="model">Thông tin đặt lại mật khẩu của người dùng.</param>
        /// <returns>
        /// Trả về kết quả đặt lại mật khẩu.
        /// </returns>
        /// <response code="200">Đặt lại mật khẩu thành công.</response>
        /// <response code="400">Đặt lại mật khẩu thất bại hoặc thông tin không hợp lệ.</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.ResetPasswordAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
