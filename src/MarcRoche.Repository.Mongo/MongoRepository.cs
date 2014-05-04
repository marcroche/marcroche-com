using System;
using System.Collections.Generic;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Repository;

namespace MarcRoche.Repository.Mongo
{
    internal class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IMongoEntity
    {
        private readonly IConfigurationService _configurationService;
        protected readonly MongoConnection<TEntity> _mongoConnection;

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
            throw new NotImplementedException();
        }

        public TEntity Get<T>(T id)
        {
            IMongoQuery entityQuery = Query<TEntity>.EQ(e => e.Id, new ObjectId(id.ToString()));
            return _mongoConnection.MongoCollection.FindOne(entityQuery);
        }

        public IEnumerable<TEntity> GetAll()
        {
            //IMongoQuery entityQuery = Query<TEntity>.All
            //return MongoConnection.MongoCollectio(entityQuery);
            return new List<TEntity>();
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
    }

    //internal interface IRepository<TEntity>
    //{
    //    TEntity Create(TEntity entity);
    //    TEntity Update(TEntity entity);
    //    TEntity Get<T>(T id);
    //    IEnumerable<TEntity> GetAll();
    //    bool Delete(string id);
    //}
}
