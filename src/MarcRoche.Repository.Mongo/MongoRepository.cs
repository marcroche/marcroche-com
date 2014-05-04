using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Repository;

namespace MarcRoche.Repository.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IMongoEntity
    {
        private readonly IConfigurationService _configurationService;
        private readonly MongoConnection<TEntity> _mongoConnection;

        public MongoRepository(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _mongoConnection = new MongoConnection<TEntity>(configurationService);
        }
 
        public TEntity Create(TEntity entity)
        {
            WriteConcernResult result = _mongoConnection.MongoCollection.Save(
                entity,
                new MongoInsertOptions
                {
                    WriteConcern = WriteConcern.Acknowledged
                });

            if (!result.Ok)
            {
                throw new Exception(string.Format("Could not create Mongo Entity: {0)", entity.Id));
            }
            //todo get the inserted entity
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            var result = _mongoConnection.MongoCollection.Update(
                    Query<TEntity>.EQ(p => p.Id, entity.Id),
                    Update<TEntity>.Replace(entity),
                    new MongoUpdateOptions
                    {
                        WriteConcern = WriteConcern.Acknowledged
                    });

            if (result.DocumentsAffected == 0)
            {
                throw new Exception("Error updating");
            }

            return entity;
        }

        public TEntity Get<T>(T id)
        {
            IMongoQuery entityQuery = Query<TEntity>.EQ(e => e.Id, new ObjectId(id.ToString()));
            return _mongoConnection.MongoCollection.FindOne(entityQuery);
        }

        public bool Delete(string id)
        {
            WriteConcernResult result = _mongoConnection.MongoCollection.Remove(
                Query<TEntity>.EQ(e => e.Id,
                new ObjectId(id)),
                RemoveFlags.None,
                WriteConcern.Acknowledged);

            if (!result.Ok)
            {
                return false;
            }
            return true;
        }

        public TEntity Get(Expression<Func<TEntity, string>> property, string value)
        {
            IMongoQuery entityQuery = Query<TEntity>.EQ(property, value);
            return _mongoConnection.MongoCollection.FindOne(entityQuery);
        }
    }
}
