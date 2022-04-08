using Catalog.Api.Entities;
using Catalog.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;


namespace Catalog.Api.Extensions
{
    public static class InitialDB
    {

        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Product>(
    map =>
    {
        map.AutoMap();
        map.MapProperty(x => x.Id)
            .SetSerializer(new GuidSerializer(BsonType.String));
    });
        }
        public static void SeedDB(this IApplicationBuilder app)
        {
            //Map();
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var sessionHandle = serviceScope.ServiceProvider.GetService<IClientSessionHandle>();
            var mongoClient = serviceScope.ServiceProvider.GetService<IMongoClient>();
            var databaseSettings = serviceScope.ServiceProvider.GetService<DatabaseSettings>();
            CreateProduct(sessionHandle, mongoClient, databaseSettings);
        }

        private static void CreateProduct(IClientSessionHandle sessionHandle, IMongoClient mongoClient, DatabaseSettings databaseSettings)
        {
            IMongoCollection<Product> productCollection =
        mongoClient.GetDatabase(databaseSettings.DatabaseName).GetCollection<Product>("Product");
            var existingProduct = productCollection.Find(a => true).Any();
            if (!existingProduct)
            {
                productCollection.InsertOne(sessionHandle, new Product()
                {
                    Categroy = "Category",
                    Description = "Description",
                    ImageFile = "Image File",
                    Name = "Name",
                    Price = 2000,
                    Summery = "Summery",
                    Id = Guid.NewGuid().ToString()
                });
            }

        }
    }
}
