using DataAccess.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public abstract class RedisRepositoryAsync<TEntity> : IRepositoryAsync<TEntity>
    {
        IDatabase _db;

        protected RedisRepositoryAsync(IDatabase db)
        {
            _db = db;
        }

        public IQueryable<TEntity> QueryAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAsync(Func<TEntity, bool> exp)
        {
            throw new NotImplementedException();
        }

        public TEntity FirstOrDefaultAsync(Func<TEntity, bool> exp)
        {
            throw new NotImplementedException();
        }

        public void AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
