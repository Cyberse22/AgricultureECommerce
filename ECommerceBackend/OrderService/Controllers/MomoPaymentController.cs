using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/payments/momo")]
    public class MomoPaymentController : ControllerBase
    {
        private readonly IMomoService _momoService;

        public MomoPaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreateMomoPaymentModel model)
        {
            var result = await _momoService.CreatePaymentAsync(model);
            return Ok(result);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var result = await _momoService.PaymentExecuteAsync(Request.Query);
            return Ok(result);
        }
    }
}