using Catalog.Api.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;
        public string _dataBase;
        public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, DatabaseSettings databaseSettings, string collection)
        {
            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(databaseSettings.DatabaseName).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(databaseSettings.DatabaseName).CreateCollection(collection);

            _dataBase = databaseSettings.DatabaseName;
        }

        protected virtual IMongoCollection<T> Collection =>
        _mongoClient.GetDatabase(_dataBase).GetCollection<T>(_collection);

        public async Task InsertAsync(T obj)
        {
            _clientSessionHandle.StartTransaction();
            await Collection.InsertOneAsync(_clientSessionHandle, obj);
            _clientSessionHandle.CommitTransaction();

        }


        public async Task UpdateAsync(T obj)
        {
            _clientSessionHandle.StartTransaction();
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            if (obj != null)
                await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
            _clientSessionHandle.CommitTransaction();
        }

        public async Task DeleteAsync(string id)
        {
            _clientSessionHandle.StartTransaction();
            await Collection.DeleteOneAsync(_clientSessionHandle, f => f.Id == id);
            _clientSessionHandle.CommitTransaction();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }
    }
}
