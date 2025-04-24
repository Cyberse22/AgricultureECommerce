namespace OrderService.Models
{
    public class OrderModel
    {
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string OrderInfo { get; set; }
        public long Amount { get; set; }
    }

    public class OrderItemModel
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public long? UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }

    public class CreateOrderModel
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; } // Momo, Cash

        public List<OrderItemModel> OrderItems { get; set; } = new();
    }
    
    public class OrderResponseModel
    {
        public string? OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }

        public List<OrderItemModel> Items { get; set; } = new();
    }
    
    public class OrderWithIndexModel
    {
        public int Index { get; set; }             
        public string? OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
    }
    public class CreateMomoPaymentModel
    {
        public string OrderId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public long Amount { get; set; }
    }
}