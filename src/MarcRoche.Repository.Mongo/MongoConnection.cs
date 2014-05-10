using System;
using System.Linq;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Repository.Mongo.Entities.Attributes;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Driver;

namespace MarcRoche.Repository.Mongo
{
    public class MongoConnection<TEntity> where TEntity : IMongoEntity
    {
        private readonly IConfigurationService _configurationService;

        public MongoCollection<TEntity> MongoCollection { get; private set; }

        public MongoConnection(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            MongoClient mongoClient = new MongoClient(_configurationService.GetApplicationSetting("mongoConnectionString"));
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase db = mongoServer.GetDatabase(_configurationService.GetApplicationSetting("database"));

            CollectionNameAttribute attribute = typeof(TEntity).GetCustomAttributes(typeof(CollectionNameAttribute), true).
                FirstOrDefault() as CollectionNameAttribute;
            if (attribute != null)
            {
                MongoCollection = db.GetCollection<TEntity>(attribute.Name);
            }
            else
            {
                throw new Exception("Could not parse Mongo Collection name.");
            }
        }
    }
}
