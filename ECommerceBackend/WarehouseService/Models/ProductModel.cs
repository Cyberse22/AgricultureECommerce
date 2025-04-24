namespace WarehouseService.Models
{
    public class GetProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductPrice { get; set; }
        public List<string> ProductImage { get; set; } = new();
        public List<string> CategoryIds { get; set; } = new();
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }

    public class CreateProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductPrice { get; set; }
        public List<string> CategoryIds { get; set; } = new();
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateProductModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductPrice { get; set; }
    }
    public class UpdateImageModel
    {
        public string ProductId { get; set; }
        public List<IFormFile> ProductImage { get; set; } = new();
    }
}
