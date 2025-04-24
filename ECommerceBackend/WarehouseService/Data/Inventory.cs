using MongoDB.Bson.Serialization.Attributes;

namespace WarehouseService.Data;

public class Inventory : BaseEntity
{
    [BsonElement]
    public string InventoryId { get; set; }
    [BsonElement]
    public string InventoryName { get; set; }
}