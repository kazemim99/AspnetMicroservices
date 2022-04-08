using Catalog.Api.Entities;
using Catalog.Api.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(
            IMongoClient mongoClient,
            IClientSessionHandle clientSessionHandle,
            DatabaseSettings settings)
            : base(mongoClient, clientSessionHandle,settings, "Product")
        {
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() =>
            await Collection.AsQueryable().ToListAsync();

        //public async Task<IEnumerable<Product>> GetBooksAsync(string ProductId)
        //{
        //    var filter = Builders<Product>.Filter.Eq(s => s.Id, ProductId);
        //    return await Collection.Find(filter).Project(p => p.Books).FirstOrDefaultAsync();
        //}

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Name, name);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}
