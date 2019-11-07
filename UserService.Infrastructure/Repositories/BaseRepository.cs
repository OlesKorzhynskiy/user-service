using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Domain;
using UserService.Infrastructure.Config;

namespace UserService.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
    {
        private readonly IMongoCollection<TEntity> _collection;
        private static string CollectionName => typeof(TEntity).Name;

        public BaseRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<TEntity>(CollectionName);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }


        public async Task<TEntity> GetAsync(string id)
        {
            try
            {
                return await _collection.Find(t => t.Id == id).SingleAsync();
            }
            catch (Exception)
            {
                throw new SystemException("Resource not found");
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            var timestamp = DateTime.UtcNow;
            entity.CreatedOn = timestamp;
            entity.UpdatedOn = timestamp;

            await _collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.UpdatedOn = DateTime.UtcNow;

            ReplaceOneResult result = await _collection.ReplaceOneAsync(
                t => t.Id == entity.Id,
                entity,
                new UpdateOptions
                {
                    IsUpsert = true
                });

            return result.IsAcknowledged && (result.ModifiedCount > 0 || result.UpsertedId != null);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.FindOneAndDeleteAsync(t => t.Id == id);
        }
    }
}