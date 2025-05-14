using CartService.Models;
using MassTransit;

namespace CartService.Services.Impl
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedMessage>
    {
        private readonly ICartService _cartService;

        public OrderCreatedConsumer(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            var message = context.Message;

            try
            {
                // Validate UserId
                if (string.IsNullOrEmpty(message.UserId))
                {
                    Console.WriteLine("Invalid OrderCreatedMessage: UserId is empty");
                    return;
                }

                // Check cart items (optional)
                var cartItems = await _cartService.GetCartItemAsync(message.UserId);
                if (cartItems == null || !cartItems.Any())
                {
                    Console.WriteLine($"No items in cart for UserId: {message.UserId}");
                    return;
                }

                // Clear cart
                var success = await _cartService.ClearCartAsync(message.UserId);
                if (success)
                {
                    Console.WriteLine($"Cart cleared for UserId: {message.UserId} after order {message.OrderId}");
                }
                else
                {
                    Console.WriteLine($"Failed to clear cart for UserId: {message.UserId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing cart for UserId: {message.UserId}. Error: {ex.Message}");
                throw; // Re-throw to let MassTransit handle retry or move to error queue
            }
        }
    }
}
