using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Get<T>(T id);
        TEntity Get(Expression<Func<TEntity, string>> property, string value);
        bool Delete(string id);
    }
}
