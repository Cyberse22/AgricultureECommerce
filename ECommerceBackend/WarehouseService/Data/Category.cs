using MongoDB.Bson.Serialization.Attributes;

namespace WarehouseService.Data;

public class Category : BaseEntity
{
    [BsonElement]
    public string CategoryId { get; set; }
    [BsonElement]
    public string CategoryName { get; set; }
    [BsonElement]
    public string CategoryParent { get; set; }
    [BsonElement]
    public string CategoryDescription { get; set; }
    [BsonElement]
    public List<string> CategoryImages { get; set; }
}