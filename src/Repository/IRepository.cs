using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
