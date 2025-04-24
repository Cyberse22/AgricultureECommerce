namespace CartService.Models
{
    public class CartItemModel
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
    }
}
