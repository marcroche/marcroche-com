﻿using System;
using MarcRoche.Repository.Mongo.Entities.Attributes;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace MarcRoche.Repository.Mongo.Entities
{
    [BsonIgnoreExtraElements]
    [CollectionName("about")]
    public class AboutEntity : MongoEntity
    {
        [BsonElement("content")] 
        public string Content { get; set; }
        [BsonElement("htmlContent")]
        public string HtmlContent { get; set; }
        [BsonElement("publishDate")]
        public DateTime PublishDate { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("aboutId")]  
        public Guid AboutId { get; set; }
    }
}
