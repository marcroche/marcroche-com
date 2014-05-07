using System.Collections.Generic;
using MarcRoche.Domain.Blog.Archive;
using MarcRoche.Repository.Mongo.Entities;
using MongoDB.Driver.Builders;
using System.Linq;

namespace MarcRoche.Repository.Mongo
{
    public class BlogRepository : MongoRepository<BlogPostEntity>
    {
        public IDictionary<int, IList<ArchiveItem>> GetArchive()
        {
            string MapFunction = 
@"function Map() {
	emit(
		new Date(this.PublishDate).getFullYear(),
		{ count: 1, publishDate: this.PublishDate, title: this.Title }
	); 
}";

            string ReduceFunction =
@"function Reduce(key, values) {
	var reduced = {count:0, posts: []};

	values.forEach(function(val) {
		reduced.posts.push({ title: val.title, publishDate: val.publishDate });
		reduced.count += val.count; 
	});

	return reduced;
}";

            string FinalizeFunction =
@"function Finalize(key, reduced) {
	return reduced;
}";
            return MapReduce<int, ArchiveItem>(MapFunction, ReduceFunction, FinalizeFunction);
        }

        public BlogPostEntity GetLatestPost()
        {
            return _mongoConnection.MongoCollection.FindAll().SetSortOrder(SortBy.Descending("PublishDate")).SetLimit(1).FirstOrDefault();
        }
    }
}
