using CartService.Models;
using CartService.Repositories;
using MassTransit;

namespace CartService.Services.Impl
{
    public class CartServiceImpl : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CartServiceImpl(ICartRepository cartRepository, IPublishEndpoint publishEndpoint)
        {
            _cartRepository = cartRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task CheckoutCartAsync(CartMessage message)
        {
            var cart = await GetCartAsync(message.UserId);
            if (cart == null || !cart.Items.Any())
            {
                throw new Exception("Cart is empty");
            }
            var checkoutRequest = new CheckoutRequest
            {
                PaymentMethod = message.PaymentMethod,
                CustomerName = message.CustomerName,
                CustomerPhone = message.CustomerPhone
            };
            // Publish the checkout request to the order service
            Console.WriteLine($"Publishing CheckoutRequest for user {message.UserId}");
            await _publishEndpoint.Publish(checkoutRequest);
            // Clear the cart after checkout
            await ClearCartAsync(message.UserId);
        }

        public async Task<bool> AddItemToCartAsync(string UserId, CartItemModel item)
        {
            var cart = await GetCartAsync(UserId);
            var existsItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existsItem != null)
            {
                existsItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }
            return await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.DeleteCartAsync(userId);
        }

        public async Task<CartModel> GetCartAsync(string UserId)
        {
            return await _cartRepository.GetCartAsync(UserId) ?? new CartModel { UserId = UserId };
        }

        public async Task<bool> RemoveItemFromCartAsync(string userId, string productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                return await _cartRepository.UpdateCartAsync(cart);
            }
            return false;
        }

        public async Task<List<CartItemModel>> GetCartItemAsync(string userId)
        {
            return await _cartRepository.GetCartItemAsync(userId);
        }
    }
}
