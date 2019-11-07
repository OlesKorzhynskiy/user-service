using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domain.UserAggregate
{
    [BsonIgnoreExtraElements]
    public class User : BaseModel
    {
        public string Name { get; set; }
    }
}