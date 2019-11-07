using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UserService.Domain;

namespace UserService.Infrastructure.EntityConfigurations
{
    public static class BaseModelConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<BaseModel>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(new StringObjectIdGenerator());
                
                cm.MapProperty(u => u.CreatedOn).SetIsRequired(true);
                cm.MapProperty(u => u.CreatedOn).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.String));

                cm.MapProperty(u => u.UpdatedOn).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.String));
            });
        }
    }
}