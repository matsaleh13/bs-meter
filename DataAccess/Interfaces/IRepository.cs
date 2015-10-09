using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Query();
        IEnumerable<TEntity> Find(Func<TEntity, bool> exp);
        TEntity FirstOrDefault(Func<TEntity, bool> exp);

        void Delete(TEntity entity);
        void Add(TEntity entity);
        void Save();
    }

    public interface IRepositoryAsync<TEntity>
    {
        IQueryable<TEntity> QueryAsync();
        IEnumerable<TEntity> FindAsync(Func<TEntity, bool> exp);
        TEntity FirstOrDefaultAsync(Func<TEntity, bool> exp);

        void DeleteAsync(TEntity entity);
        void AddAsync(TEntity entity);
        void SaveAsync();
    }

}
