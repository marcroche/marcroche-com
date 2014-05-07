using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Domain.Blog.Archive;
using MarcRoche.Repository.Mongo.Entities;
using Repository;

namespace MarcRoche.Repository.Mongo
{
    public interface IBlogRepository<TEntity> : IRepository<TEntity>
    {
        IDictionary<int, IList<ArchiveItem>> GetArchive();
        BlogPostEntity GetLatestPost();
    }
}
