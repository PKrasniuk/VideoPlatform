using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities
{
    public class MetaData : MetaEntity
    {
        public BsonString Name { get; set; }

        public BsonString Description { get; set; }

        public BsonString Value { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MetaType Type { get; set; }
    }
}