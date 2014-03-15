using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Caching;
using Newtonsoft.Json;
using Repository;

namespace MarcRoche.Repository.FileSystem
{
    public class FileRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static readonly string CacheKey = typeof(TEntity).ToString();
        private string extension = ".json";
        private string _path;

        public FileRepository(string path)
        {
            _path = path;
        }

        public IEnumerable<TEntity> AllFiles 
        {
            get 
            {
                if (MemoryCache.Default.Contains(FileRepository<TEntity>.CacheKey))
                {
                    return (IEnumerable<TEntity>)MemoryCache.Default.Get(FileRepository<TEntity>.CacheKey);
                }

                MemoryCache.Default.Add(
                    new CacheItem(FileRepository<TEntity>.CacheKey, GetItems()),
                    new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(24)
                    });

                return (IEnumerable<TEntity>)MemoryCache.Default.Get(FileRepository<TEntity>.CacheKey);
            }
        }

        private IEnumerable<TEntity> GetItems()
        {
            string extension = "*.json";
            Collection<TEntity> items = new Collection<TEntity>();
            foreach (string file in Directory.EnumerateFiles(_path, "*" + extension))
            {
                items.Add(JsonConvert.DeserializeObject<TEntity>(File.ReadAllText(file)));
            }
            return items;
        }

        public TEntity Get(Guid id)
        {
            return JsonConvert.DeserializeObject<TEntity>(
                File.ReadAllText(
                    Path.Combine(_path, id.ToString() + extension)));
        }

        public TEntity Get(int id)
        {
            return JsonConvert.DeserializeObject<TEntity>(
                File.ReadAllText(
                    Path.Combine(_path, id.ToString() + extension)));
        }

        public TEntity Get(string id)
        {
            if (!File.Exists(Path.Combine(_path, id + extension)))
            {
                return default(TEntity);
            }

            return JsonConvert.DeserializeObject<TEntity>(
                File.ReadAllText(
                    Path.Combine(_path, id + extension)));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return AllFiles;
        }


        public TEntity Create<TKey>(TKey key, TEntity entity)
        {
            File.WriteAllText(Path.Combine(_path, key + extension), JsonConvert.SerializeObject(entity));
            return entity;
        }
    }
}

