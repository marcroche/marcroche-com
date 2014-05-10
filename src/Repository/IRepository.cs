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
        TEntity GetLatest();
        IDictionary<T, IList<V>> MapReduce<T, V>(string map, string reduce, string finalize);
        bool Delete(string id);
    }
}
