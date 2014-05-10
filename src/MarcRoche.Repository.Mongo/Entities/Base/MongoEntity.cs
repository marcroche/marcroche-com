using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarcRoche.Repository.Mongo.Entities.Base
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        [BsonElement("_id")] 
        public ObjectId Id { get; set; }

        public TModel As<TModel, TEntity>() where TEntity : class
        {
            return Mapper.Map<TEntity, TModel>(this as TEntity);
        }
    }
}
