using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Domain.Blog;
using MarcRoche.Repository.Mongo.Entities;
using MarcRoche.Repository.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Repository;

namespace MarcRoche.Repository.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IMongoEntity
    {
        private readonly IConfigurationService _configurationService;
        private readonly MongoConnection<TEntity> _mongoConnection;

        static MongoRepository()
        {
            Mapper.CreateMap<AboutEntity, About>();
            Mapper.CreateMap<BlogPostEntity, BlogPost>();
            Mapper.CreateMap<BlogCommentEntity, BlogComment>();
        }

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

        public IDictionary<T, IList<V>> MapReduce<T,V>(string map, string reduce, string finalize)
        {
            var options = new MapReduceOptionsBuilder();
            options.SetFinalize(finalize);

            var results = _mongoConnection.MongoCollection.MapReduce(map, reduce, options);

            IDictionary<T, IList<V>> dict = new Dictionary<T, IList<V>>();
            foreach (var result in results.GetResults())
            {
                dynamic obj = JsonConvert.DeserializeObject(result.ToJson());
                if(!dict.ContainsKey((T)obj._id))
                {
                    dict.Add((T)obj._id, new List<V>());
                }

                foreach(var post in obj.value.posts)
                {
                    V v = JsonConvert.DeserializeObject<V>(post.ToString());
                    dict[(T)obj._id].Add(v);
                }
            }

            return dict;
        }
    }
}
