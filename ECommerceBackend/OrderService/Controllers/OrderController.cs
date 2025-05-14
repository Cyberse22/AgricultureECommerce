using AutoMapper;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IOrderService orderService, IPublishEndpoint publishEndpoint)
        {
            _orderService = orderService;
            _publishEndpoint = publishEndpoint;
        }

        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetOrdersByUserId(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return BadRequest("UserId is required");
        //    }

        //    try
        //    {
        //        var orders = await _orderService.GetOrdersByUserIdAsync(userId);
        //        if (orders == null || !orders.Any())
        //        {
        //            return NotFound("No orders found for this user");
        //        }

        //        // Publish OrderQueriedMessage
        //        await _publishEndpoint.Publish(new OrderQueriedMessage
        //        {
        //            UserId = userId,
        //            OrderCount = orders.Count,
        //            Timestamp = DateTime.UtcNow
        //        });

        //        return Ok(orders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error retrieving orders: {ex.Message}");
        //    }
        //}

        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest("OrderId is required");
            }

            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound("Order not found");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving order details: {ex.Message}");
            }
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest("OrderId is required");
            }

            try
            {
                var result = await _orderService.CancelOrderAsync(orderId);
                if (!result)
                {
                    return NotFound("Order not found or cannot be cancelled");
                }
                return Ok(new { Message = "Order cancelled successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error cancelling order: {ex.Message}");
            }
        }
    }
}