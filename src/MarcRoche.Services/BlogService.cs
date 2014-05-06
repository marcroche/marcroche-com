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
using MarcRoche.Domain.Services;

namespace MarcRoche.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogPostEntity> _blogRepository;
        private readonly IRepository<AboutEntity> _aboutMeRepository;
        private readonly IRepository<IList<BlogCommentEntity>> _blogCommentRepository;

        public BlogService(
            IRepository<BlogPostEntity> blogRepository,
            IRepository<AboutEntity> aboutMeRepository,
            IRepository<IList<BlogCommentEntity>> blogCommentRepository)
        {
            _blogRepository = blogRepository;
            _aboutMeRepository = aboutMeRepository;
            _blogCommentRepository = blogCommentRepository;
        }

        public IEnumerable<BlogPost> GetArchive(int year, int month)
        {
            return _blogRepository.GetAll().Where(x => x.PublishDate.Year == year && x.PublishDate.Month == month);
        }

        public BlogPost GetLatestPost()
        {
            return _blogRepository.GetAll().OrderByDescending(x => x.PublishDate).FirstOrDefault();
        }

        public IEnumerable<BlogPost> Search(string searchText)
        {
            return _blogRepository.GetAll().Where(x => x.Content.Contains(searchText));
        }

        public BlogPost GetPostByTitle(string title)
        {
            _blogRepository.Get(x => x.Title, title.ToLower().Replace(" ", "-"));
            return _blogRepository.GetAll().FirstOrDefault(x => x.Title.ToUpper() == title.ToUpper());
        }

        public IEnumerable<BlogPost> GetAll()
        {
            foreach (BlogPost post in _blogRepository.GetAll())
            {
                yield return post;
            }
        }

        public void CreateComment(string title, BlogComment blogComment)
        {
            //blogComment.Date = DateTime.UtcNow;
            //blogComment.Id = Guid.NewGuid();
            //blogComment.EmailHash = HashEmailForGravatar(blogComment.Email);
            //blogComment.Content = HtmlSanitizer.SanitizeHtml(blogComment.Content);

            //IList<BlogComment> comments = _blogCommentRepository.Get(title.ToLower().Replace(" ", "-"));

            //if(comments == null)
            //{
            //    comments = new List<BlogComment>(); 
            //}

            //comments.Add(blogComment);
            //_blogCommentRepository.Create(title.ToLower().Replace(" ", "-"), comments);
        }

        private static string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }


        public IList<BlogComment> GetComments(string title)
        {
            throw new NotImplementedException();
            //title = title.ToLower().Replace(" ", "-");
            //return _blogCommentRepository.Get(title).Get;
        }
    }
}
