using CartService.Models;
using CartService.Services;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, IPublishEndpoint publishEndpoint, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required");
            }

            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItemToCart(string userId, CartItemModel item)
        {
            var result = await _cartService.AddItemToCartAsync(userId, item);
            return result ? Ok(result) : BadRequest();
        }
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemToCart(string userId, string productId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(userId, productId);
            return result ? Ok(result) : NotFound();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            return result ? Ok(result) : BadRequest();
        }

        [HttpGet("{userId}/items")]
        public async Task<IActionResult> GetCartItemsAsync(string userId)
        {
            var result = await _cartService.GetCartItemAsync(userId);
            return Ok(result);
        }
        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(string userId, [FromBody] CheckoutRequest request)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Checkout failed: UserId is required");
                return BadRequest("UserId is required");
            }

            var cart = await _cartService.GetCartAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                _logger.LogWarning("Checkout failed: Cart not found or empty for UserId: {UserId}", userId);
                return BadRequest("Cart not found or empty");
            }

            if (cart.UserId != userId)
            {
                _logger.LogWarning("Checkout failed: UserId mismatch for UserId: {UserId}", userId);
                return BadRequest("UserId mismatch in cart");
            }

            try
            {
                var message = new CartMessage
                {
                    UserId = userId,
                    PaymentMethod = request.PaymentMethod ?? "Cash",
                    CustomerName = request.CustomerName,
                    CustomerPhone = request.CustomerPhone,
                    Items = cart.Items
                };

                _logger.LogInformation("Preparing to publish CartMessage: {@CartMessage}", message);
                await _publishEndpoint.Publish(message);
                _logger.LogInformation("CartMessage published to cart-queue for UserId: {UserId} at {Timestamp}", userId, DateTime.UtcNow);
                return Ok(new { Message = "Checkout initiated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing CartMessage for UserId: {UserId} at {Timestamp}", userId, DateTime.UtcNow);
                return StatusCode(500, $"Error during checkout: {ex.Message}");
            }
        }
    }
}
