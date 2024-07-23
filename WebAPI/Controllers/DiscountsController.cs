using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.DiscountModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var discounts = await _discountService.GetAllDiscountsAsync();
            return Ok(discounts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var discounts = await _discountService.GetDiscountByIdAsync(id);
            return Ok(discounts);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DiscountForCreate discountDto)
        {
            var discount = await _discountService.CreateDiscountAsync(discountDto);
            return Ok(discount);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DiscountForUpdate discountDto, Guid id)
        {
            var discount = await _discountService.UpdateDiscountAsync(discountDto, id);
            return Ok(discount);
        }
    }
}

