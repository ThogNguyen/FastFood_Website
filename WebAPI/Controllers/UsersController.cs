using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.UserModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        // mặc định của asp.net User Id là string nên giờ chuyển lại sang guid
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var users = await _userService.GetUserByIdAsync(id.ToString());
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserForCreate userDto)
        {
            var user = await _userService.CreateUserAsync(userDto);
            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserForUpdate userDto, Guid id)
        {
            var user = await _userService.UpdateUserAsync(userDto, id.ToString());
            return Ok(user);
        }


    }
}
