using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Get<T>(T id);
        IEnumerable<TEntity> GetAll();
        bool Delete(string id);

        //TEntity Get(Guid id);
        //TEntity Get(int id);
        //TEntity Get(string id);
        //IEnumerable<TEntity> GetAll();
        //TEntity Create<TKey>(TKey key, TEntity entity);
    }
}
