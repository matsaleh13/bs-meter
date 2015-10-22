using DataAccess.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class RedisRepositoryAsync<TEntity> : IRepositoryAsync<TEntity>
        where TEntity : IEntity, new()
    {
        IConnectionMultiplexer _conn;
        IDatabase _db;

        protected RedisRepositoryAsync(IConnectionMultiplexer conn)
        {
            _conn = conn;
            _db = _conn.GetDatabase();
        }

        async public Task<TEntity> GetAsync(TEntity query)
        {
            var hashEntries = await _db.HashGetAllAsync(query.ToRedisKey()).ConfigureAwait(false);
            var entity = RedisEntityMapper<TEntity>.ToEntity(query.ToRedisKey(), hashEntries);
            return entity;
        }

        async public void AddAsync(TEntity entity)
        {
            var hashEntries = RedisEntityMapper<TEntity>.ToHashEntries(entity);

            // TODO: create key.
            await _db.HashSetAsync(entity.ToRedisKey(), hashEntries).ConfigureAwait(false);
        }

        async public void DeleteAsync(TEntity entity)
        {
            await _db.KeyDeleteAsync(entity.ToRedisKey());
        }

        public void SaveAsync()
        {
            
        }
    }
}
