using MarcRoche.Repository.Mongo.Entities.Attributes;
using MarcRoche.Repository.Mongo.Entities.Base;

namespace MarcRoche.Repository.Mongo.Entities
{
    [CollectionName("comments")]
    public class BlogCommentEntity : MongoEntity
    {
    }
}
