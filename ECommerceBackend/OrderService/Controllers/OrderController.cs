using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;


namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("order-id")]
        public async Task<IActionResult> GetOrderId(string orderId)
        {
            var result = await _orderService.GetOrderIdAsync(orderId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            var result = await _orderService.CreateOrderAsync(model);
            return CreatedAtAction(nameof(GetOrderId), new { id = result.OrderId }, result);
        }
        [HttpGet("by-date")]
        public async Task<IActionResult> GetOrdersByDate([FromQuery] DateTime date)
        {
            var orders = await _orderService.GetAllOrdersByDateAsync(date);
            return Ok(orders);
        }

        [HttpGet("by-date-with-index")]
        public async Task<IActionResult> GetOrdersByDateWithIndex([FromQuery] DateTime date)
        {
            var orders = await _orderService.GetOrdersByDateWithIndex(date);
            return Ok(orders);
        }
    }
}