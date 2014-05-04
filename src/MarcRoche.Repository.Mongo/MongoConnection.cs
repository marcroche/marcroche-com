using MarcRoche.Common.Infrastructure;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Driver;

namespace MarcRoche.Repository.Mongo
{
    internal class MongoConnection<TEntity> where TEntity : IMongoEntity
    {
        private readonly IConfigurationService _configurationService;

        public MongoCollection<TEntity> MongoCollection { get; private set; }

        public MongoConnection(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            MongoClient mongoClient = new MongoClient(_configurationService.GetApplicationSetting("mongoConnectionString"));
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase db = mongoServer.GetDatabase(_configurationService.GetApplicationSetting("database"));
            MongoCollection = db.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }
    }
}
