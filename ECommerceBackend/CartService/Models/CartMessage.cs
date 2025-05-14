namespace CartService.Models
{
    public record CartMessage
    {
        public string UserId { get; set; }
        public string PaymentMethod { get; set; }
        public List<CartItemModel> Items { get; set; } = new();
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
    }
    public record OrderCreatedMessage
    {
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
