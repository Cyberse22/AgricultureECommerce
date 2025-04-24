namespace WarehouseService.Models
{
    public class InventoryModel
    {
        public string InventoryId { get; set; }
        public string InventoryName { get; set; }
    }

    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public string InventoryId { get; set; }
        public string Type { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public List<ProductTransactionModel> Products { get; set; }
    }

    public class ProductTransactionModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}