using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        TEntity Get(string key);
        TEntity Get(TEntity entity);
        bool Add(TEntity entity);
        bool Delete(string key);
        bool Delete(TEntity entity);
    }


    public interface IRepositoryAsync<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> GetAsync(string key);
        Task<TEntity> GetAsync(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(string key);
        Task<bool> DeleteAsync(TEntity entity);
    }

}
