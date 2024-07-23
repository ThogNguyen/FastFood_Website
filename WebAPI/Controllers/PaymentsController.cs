using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.PaymentModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var payments = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(payments);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentForCreate paymentDto)
        {
            var payments = await _paymentService.CreatePaymentAsync(paymentDto);
            return Ok(payments);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PaymentForUpdate paymentDto, Guid id)
        {
            var payments = await _paymentService.UpdatePaymentAsync(paymentDto, id);
            return Ok(payments);
        }
    }
}
