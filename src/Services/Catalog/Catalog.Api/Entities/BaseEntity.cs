using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.Api.Data
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; }
    }
}