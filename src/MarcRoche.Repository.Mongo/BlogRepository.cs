using System.Collections.Generic;
using System.Linq;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Domain.Blog.Archive;
using MarcRoche.Repository.Mongo.Entities;
using MongoDB.Driver.Builders;

namespace MarcRoche.Repository.Mongo
{
    public class BlogRepository : MongoRepository<BlogPostEntity>, IBlogRepository<BlogPostEntity>
    {
        private readonly IConfigurationService _configurationService;

        public BlogRepository(IConfigurationService configurationService) : base(configurationService)
        {
            _configurationService = configurationService;
        }

        public IDictionary<int, IList<ArchiveItem>> GetArchive()
        {
            string MapFunction = 
@"function Map() {
	emit(
		new Date(this.publishDate).getFullYear(),
		{ count: 1, publishDate: this.publishDate, title: this.title }
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
            return _mongoConnection.MongoCollection.FindAll().SetSortOrder(SortBy.Descending("publishDate")).SetLimit(1).FirstOrDefault();
        }
    }
}
