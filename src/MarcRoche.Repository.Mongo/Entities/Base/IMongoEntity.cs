using MongoDB.Bson;

namespace MarcRoche.Repository.Mongo.Entities.Base
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
