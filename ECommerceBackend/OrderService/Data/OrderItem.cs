using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data
{
    public class OrderItem : BaseEntity
    {
        [ForeignKey("Order")]
        public string? OrderCode { get; set; } = null;
        public Order? Order { get; set; }
        
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public long? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Discount { get; set; }
    }
}
