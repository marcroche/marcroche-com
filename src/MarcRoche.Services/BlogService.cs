using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using MarcRoche.Domain.Services;
using Repository;
using System.Linq;
using MarcRoche.Domain.Blog;
using System.Security.Cryptography;
using System.Text;
using MarcRoche.Common;
using MarcRoche.Repository.Mongo.Entities;
using MarcRoche.Domain.Blog.Archive;
using MarcRoche.Repository.Mongo;
using MarcRoche.Common.Comparers;

namespace MarcRoche.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogPostEntity> _blogRepository;
        private readonly IBlogRepository<BlogPostEntity> _repo;

        public BlogService(
            IRepository<BlogPostEntity> blogRepository,
            IBlogRepository<BlogPostEntity> repo)
        {
            _blogRepository = blogRepository;
            _repo = repo;
        }

        public IDictionary<int, IList<ArchiveItem>> GetArchive()
        {
            return new SortedDictionary<int, IList<ArchiveItem>>(_repo.GetArchive(), new ReverseComparer<int>());
        }

        public BlogPost GetLatestPost()
        {
            return _repo.GetLatestPost().As<BlogPost, BlogPostEntity>();
        }

        public BlogPost GetPostByTitle(string title)
        {
            return _repo.Get(x => x.SearchableTitle, title.ToLower().Replace(" ", "-")).As<BlogPost, BlogPostEntity>();
        }

        private static string HashEmailForGravatar(string email)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));  
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString(); 
        }
    }
}
