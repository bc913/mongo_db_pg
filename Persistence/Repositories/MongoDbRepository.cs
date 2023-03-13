using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Shine.Backend.Application.Contracts.Repositories;
using Shine.Backend.Persistence.Contracts.Settings;
using Shine.Backend.Common.Interfaces;

namespace Shine.Backend.Persistence.Repositories
{
    public class MongoDbRepository<T> : IRepository<T> 
        where T : class, IAggregateRootWithId<string>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(IEntityStoreMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            _collection = db.GetCollection<T>(settings.CollectionName);
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(entity, null, cancellationToken);
            return entity;
        }

        public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(d => d.Id, id);
            await _collection.DeleteOneAsync(filter, cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(d => d.Id, id);
            //var filter2 = Builders<T>.Filter.Eq("id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Empty;
            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, entity.Id);
            var updateResult = await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions(){IsUpsert = false}, cancellationToken);
        }
    }
}