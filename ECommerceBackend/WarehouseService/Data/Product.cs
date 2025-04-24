using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WarehouseService.Data
{
    public class Product : BaseEntity
    {
        [BsonElement]
        public string? ProductId { get; set; }
        [BsonElement]
        public string? ProductName { get; set; }
        [BsonElement]
        public string ProductDescription { get; set; } = string.Empty;
        [BsonElement]
        public long? ProductPrice { get; set; }
        [BsonElement]
        public List<string>? ProductImage { get; set; } = new();

        [BsonElement] 
        public List<string>? CategoryIds { get; set; } = new();
        [BsonElement]
        public string? InventoryId { get; set; }
        [BsonElement]
        public int Quantity { get; set; }
        [BsonElement]
        public int AvailableQuantity { get; set; }
        [BsonElement]
        public int ReservedQuantity  { get; set; }
    }
}
