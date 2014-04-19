using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<TEntity>
    {
        TEntity Get(Guid id);
        TEntity Get(int id);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        TEntity Create<TKey>(TKey key, TEntity entity);
    }
}
