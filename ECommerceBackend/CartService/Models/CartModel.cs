using Shared.Messaging;

namespace CartService.Models
{
    public class CartModel
    {
        public string UserId { get; set; } = null!;
        public List<CartItemModel> Items { get; set; } = new();
    }
    public class CheckoutRequest
    {
        public string? PaymentMethod { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
    }
}
