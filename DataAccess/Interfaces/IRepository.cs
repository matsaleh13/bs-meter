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
        TEntity Get(TEntity entity);
        void Delete(TEntity entity);
        void Add(TEntity entity);
    }


    public interface IRepositoryAsync<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> GetAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        void AddAsync(TEntity entity);
    }

}
