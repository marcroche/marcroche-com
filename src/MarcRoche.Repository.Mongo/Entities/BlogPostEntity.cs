using System;
using System.Collections.Generic;
using MarcRoche.Repository.Mongo.Entities.Attributes;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace MarcRoche.Repository.Mongo.Entities
{
    [BsonIgnoreExtraElements]
    [CollectionName("posts")]
    public class BlogPostEntity : MongoEntity
    {
        [BsonElement("author")] 
        public string Author { get; set; }
        [BsonElement("content")] 
        public string Content { get; set; }
        [BsonElement("htmlContent")] 
        public string HtmlContent { get; set; }
        [BsonElement("markdownContent")] 
        public string MarkdownContent { get; set; }
        [BsonElement("blogPostId")] 
        public Guid BlogPostId { get; set; }
        [BsonElement("publishDate")] 
        public DateTime PublishDate { get; set; }
        [BsonElement("tags")] 
        public IEnumerable<string> Tags { get; set; }
        [BsonElement("title")] 
        public string Title { get; set; }
        [BsonElement("searchableTitle")]
        public string SearchableTitle { get; set; }
        [BsonElement("comments")] 
        public IEnumerable<BlogCommentEntity> Comments { get; private set; }
        [BsonElement("scriptDependencies")] 
        public IEnumerable<string> ScriptDependencies { get; set; }

        public BlogPostEntity()
        {
            Comments = new List<BlogCommentEntity>();
            ScriptDependencies = new List<string>();
            Tags = new List<string>();
        }
    }
}