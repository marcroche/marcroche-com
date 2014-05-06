using MarcRoche.Domain.Blog.Archive;
using MarcRoche.Repository.Mongo.Entities;

namespace MarcRoche.Repository.Mongo
{
    public class BlogRepository : MongoRepository<BlogPostEntity>
    {
        public object GetArchive()
        {
            return MapReduce<int, ArchiveItem>(Archive.MapFunction, Archive.ReduceFunction, Archive.FinalizeFunction);
        }
    }
}
