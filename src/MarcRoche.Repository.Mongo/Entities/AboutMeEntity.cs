using System;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace MarcRoche.Repository.Mongo.Entities
{
    [BsonIgnoreExtraElements]
    internal class AboutMeEntity : MongoEntity
    {
        [BsonElement("author")] 
        public string Author { get; set; }
        [BsonElement("content")] 
        public string Content { get; set; }
        [BsonElement("htmlContent")]
        public string HtmlContent { get; set; }
        [BsonElement("publishDate")]
        public DateTime PublishDate { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("aboutMeId")]  
        public Guid AboutMeId { get; set; }
    }
}
