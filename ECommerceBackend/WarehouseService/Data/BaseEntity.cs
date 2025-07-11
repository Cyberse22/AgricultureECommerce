﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WarehouseService.Data
{
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("createddate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [BsonElement("updateddate")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
