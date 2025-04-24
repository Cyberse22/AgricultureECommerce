namespace OrderService.Data
{
    public class Order : BaseEntity
    {
        public string? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? Status { get; set; } // Pending, Failed, Paid, Cancelled
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; } // Momo, VNPAY, Cash
        public string? PaymentTransactionId { get; set; }
        public string? PaymentUrl { get; set; }
        public int Index { get; set; }
        
        public List<OrderItem> Items { get; set; } = new(); 
    }
}
