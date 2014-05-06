using System.Collections.Generic;

namespace MarcRoche.Domain.Blog.Archive
{
    public class Archive
    {
        public static string MapFunction = 
@"function Map() {
	emit(
		new Date(this.PublishDate).getFullYear(),
		{ count: 1, publishDate: this.PublishDate, title: this.Title }
	); 
}";

        public static string ReduceFunction =
@"function Reduce(key, values) {
	var reduced = {count:0, posts: []};

	values.forEach(function(val) {
		reduced.posts.push({ title: val.title, publishDate: val.publishDate });
		reduced.count += val.count; 
	});

	return reduced;
}";

        public static string FinalizeFunction =
@"function Finalize(key, reduced) {
	return reduced;
}";
    }
}
