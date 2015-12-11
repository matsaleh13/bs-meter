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

        protected void CheckParam(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrEmpty(entity.Key))
            {
                throw new ArgumentNullException(nameof(entity.Key), "Parameter is null or empty.");
            }
        }


        protected void CheckParam(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentNullException(nameof(param), "Parameter is null or empty.");
            }
        }


        protected RedisRepositoryAsync(ICacheClient client)
        {
            _client = client;
        }

        async public Task<TEntity> GetAsync(string key)
        {
            CheckParam(key);
            var entity = await _client.GetAsync<TEntity>(key).ConfigureAwait(false);
            return entity;
        }

        async public Task<TEntity> GetAsync(TEntity entity)
        {
            CheckParam(entity);
            return await GetAsync(entity.Key).ConfigureAwait(false);
        }

        async public Task<bool> AddAsync(TEntity entity)
        {
            CheckParam(entity);
            return await _client.AddAsync(entity.Key, entity).ConfigureAwait(false);
        }

        async public Task<bool> DeleteAsync(string key)
        {
            CheckParam(key);
            return await _client.RemoveAsync(key);
        }

        async public Task<bool> DeleteAsync(TEntity entity)
        {
            CheckParam(entity);
            return await _client.RemoveAsync(entity.Key);
        }
    }
}
