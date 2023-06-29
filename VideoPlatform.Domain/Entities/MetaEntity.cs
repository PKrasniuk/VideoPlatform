using MongoDB.Bson;

namespace VideoPlatform.Domain.Entities;

public abstract class MetaEntity : BaseEntity<ObjectId>
{
    public BsonDateTime CreatedAt { get; set; }

    public BsonDateTime UpdatedAt { get; set; }
}