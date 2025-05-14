using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public record CartMessage
    {
        public string UserId { get; set; } = null!;
        public string PaymentMethod { get; set; } = "Cash";
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public List<CartItemModel> Items { get; set; } = new();
    }
    public class CartItemModel
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
    public class CheckoutRequest
    {
        public string? PaymentMethod { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
    }
}
