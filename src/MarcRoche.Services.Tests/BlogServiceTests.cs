using System;
using System.Collections.Generic;
using System.Linq;
using MarcRoche.Domain.Blog;
using MarcRoche.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;

namespace MarcRoche.Services.Tests
{
    [TestClass]
    public class BlogServiceTests
    {
        [TestMethod]
        public void Can_GetAboutMe_Test()
        {
            IBlogService blogService = new BlogService(_blogPostRepo.Object, _aboutMeRepo.Object, _blogCommentRepository.Object);

            AboutMe actual = blogService.GetAboutMe();

            Assert.AreEqual("test-user", actual.Author);
            Assert.AreEqual("Test about me content", actual.Content);
            Assert.AreEqual(AboutMe.CacheKey, actual.Id);
            Assert.AreEqual(DateTime.Parse("1/1/2013"), actual.PublishDate);
            Assert.AreEqual("About Me Title", actual.Title);
        }

        [TestMethod]
        public void Can_GetArchive_Test()
        {
            IBlogService blogService = new BlogService(_blogPostRepo.Object, _aboutMeRepo.Object, _blogCommentRepository.Object);

            IEnumerable<BlogPost> actual = blogService.GetArchive(2013, 1);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("Test Blog Title 1", actual.First().Title);
        }

        [TestMethod]
        public void Can_GetLatestPost_Test()
        {
            IBlogService blogService = new BlogService(_blogPostRepo.Object, _aboutMeRepo.Object, _blogCommentRepository.Object);

            BlogPost actual = blogService.GetLatestPost();

            Assert.AreEqual("Test Blog Title 2", actual.Title);
        }

        [TestMethod]
        public void Can_GetPostByTitle_Test()
        {
            IBlogService blogService = new BlogService(_blogPostRepo.Object, _aboutMeRepo.Object, _blogCommentRepository.Object);

            BlogPost actual = blogService.GetPostByTitle("Test Blog Title 2");

            Assert.AreEqual("Test Blog Title 2", actual.Title);
        }

        [TestMethod]
        public void Can_GetComments_Test()
        {
            IBlogService blogService = new BlogService(_blogPostRepo.Object, _aboutMeRepo.Object, _blogCommentRepository.Object);

            IEnumerable<BlogComment> actual = blogService.GetComments("test-blog-comments");

            Assert.AreEqual(1, actual.Count());
        }


        /// TEST CLASS SETUP CODE
        private static Mock<IRepository<BlogPost>> _blogPostRepo;
        private static Mock<IRepository<AboutMe>> _aboutMeRepo;
        private static Mock<IRepository<IList<BlogComment>>> _blogCommentRepository;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext context)
        {
            _blogPostRepo = new Mock<IRepository<BlogPost>>();
            _aboutMeRepo = new Mock<IRepository<AboutMe>>();
            _blogCommentRepository = new Mock<IRepository<IList<BlogComment>>>();

            _blogPostRepo.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<BlogPost>
                {
                    new BlogPost 
                    { 
                        Author = "test-user-1",
                        Content = "Test blog content 1",
                        Id = Guid.Parse("AB376F83-D8C4-4E67-BA19-68640771F753"),
                        PublishDate = DateTime.Parse("1/1/2013"),
                        Title = "Test Blog Title 1"
                    },
                    new BlogPost 
                    { 
                        Author = "test-user-2",
                        Content = "Test blog content 2",
                        Id = Guid.Parse("677400C9-8ED0-43F7-A2D2-7025D38B8069"),
                        PublishDate = DateTime.Parse("2/1/2013"),
                        Title = "Test Blog Title 2"
                    }
                };
            });

            _blogPostRepo.Setup(x => x.Get(Guid.Parse("677400C9-8ED0-43F7-A2D2-7025D38B8069"))).Returns(() =>
            {
                return new BlogPost
                {
                    Author = "test-user-2",
                    Content = "Test blog content 2",
                    Id = Guid.Parse("677400C9-8ED0-43F7-A2D2-7025D38B8069"),
                    PublishDate = DateTime.Parse("1/1/2013"),
                    Title = "Test Blog Title 2"
                };
            });

            _aboutMeRepo.Setup(x => x.Get(AboutMe.CacheKey)).Returns(() =>
            {
                return new AboutMe
                {
                    Author = "test-user",
                    Content = "Test about me content",
                    Id = AboutMe.CacheKey,
                    PublishDate = DateTime.Parse("1/1/2013"),
                    Title = "About Me Title"
                };
            });

            _aboutMeRepo.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<AboutMe>
                {
                    new AboutMe
                    {
                        Author = "test-user",
                        Content = "Test about me content",
                        Id = AboutMe.CacheKey,
                        PublishDate = DateTime.Parse("1/1/2013"),
                        Title = "About Me Title"
                    }
                };
            });

            _blogCommentRepository.Setup(x => x.Get("test-blog-comments")).Returns(() =>
            {
                return new List<BlogComment>
                {
                    new BlogComment
                    {
                        Author = "marcroche",
                        Content = "Great post",
                        Date = DateTime.UtcNow,
                        Email = "none",
                        HomePage = "none",
                        Id = Guid.Empty,
                        IsModerated = false
                    }
                };
            });
        }

    }
}
