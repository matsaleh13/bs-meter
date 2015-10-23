using DataAccess.Interfaces;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class RedisRepositoryAsync<TEntity> : IRepositoryAsync<TEntity>
        where TEntity : class, IEntity, new()
    {
        ICacheClient _client;

        protected void CheckEntityParam(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrEmpty(entity.Key))
            {
                throw new ArgumentNullException(nameof(entity), "entity.Key is null or empty.");
            }
        }


        protected RedisRepositoryAsync(ICacheClient client)
        {
            _client = client;
        }

        async public Task<TEntity> GetAsync(string key)
        {
            var entity = await _client.GetAsync<TEntity>(key).ConfigureAwait(false);
            return entity;
        }

        async public Task<TEntity> GetAsync(TEntity entity)
        {
            CheckEntityParam(entity);
            return await GetAsync(entity.Key).ConfigureAwait(false);
        }

        async public Task<bool> AddAsync(TEntity entity)
        {
            CheckEntityParam(entity);
            return await _client.AddAsync(entity.Key, entity).ConfigureAwait(false);
        }

        async public Task<bool> DeleteAsync(string key)
        {
            return await _client.RemoveAsync(key);
        }

        async public Task<bool> DeleteAsync(TEntity entity)
        {
            CheckEntityParam(entity);
            return await _client.RemoveAsync(entity.Key);
        }
    }
}
