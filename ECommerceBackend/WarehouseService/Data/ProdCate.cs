using MongoDB.Bson.Serialization.Attributes;

namespace WarehouseService.Data;

public class ProdCate
{
    [BsonElement]
    public string? CategoryId { get; set; }
    [BsonElement]
    public string? ProductId { get; set; }
    [BsonElement]
    public DateTime UpdatedDate { get; set; }
}