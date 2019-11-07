using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace UserService.Domain
{
    [BsonIgnoreExtraElements]
    public class BaseModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.String)]
        public DateTime CreatedOn { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.String)]
        public DateTime? UpdatedOn { get; set; }

        [JsonIgnore]
        public int Version { get; set; }
    }
}