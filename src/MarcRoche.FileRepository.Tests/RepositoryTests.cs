using System;
using System.Collections.Generic;
using System.Linq;
using MarcRoche.Model.Blog;
using MarcRoche.Repository.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace MarcRoche.FileRepository.Tests
{
    [TestClass]
    [DeploymentItem(@"FileData\2471ecc5-256e-4ea9-accf-d74a652015d4.json", @"App_Data\blog")]
    [DeploymentItem(@"FileData\5a5fcdfb-4d2d-4aed-a442-cd72054cef5c.json", @"App_Data\blog")]
    [DeploymentItem(@"FileData\6efdcf97-2151-41a9-b65e-f7e3ceb24db2.json", @"App_Data\blog")]
    [DeploymentItem(@"FileData\test-blog-comments.json", @"App_Data\comments")]
    public class RepositoryTests
    {
        private static IRepository<BlogPost> _repo;
        private static IRepository<List<BlogComment>> _blogCommentsRepository;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "App_Data");
            _repo = new FileRepository<BlogPost>(Settings.BlogFileDbPath);
            _blogCommentsRepository = new FileRepository<List<BlogComment>>(Settings.CommentsFileDbPath);
        }

        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<BlogPost> posts = _repo.GetAll();
            Assert.IsTrue(posts.Count() == 3);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            BlogPost post = _repo.Get(Guid.Parse("2471ecc5-256e-4ea9-accf-d74a652015d4"));
            Assert.IsTrue(post.Title == "About Me");
        }

        [TestMethod]
        public void CreateCommentTest()
        {
            List<BlogComment> comments = _blogCommentsRepository.Get("test-blog-comments-empty");

            if (comments == null)
            {
                comments = new List<BlogComment>();
            }

            comments.Add(new BlogComment
            {
                Author = "marcroche",
                Content = "Great post",
                Date = DateTime.UtcNow,
                Email = "none",
                HomePage = "none",
                Id = Guid.Empty,
                IsModerated = false
            });
            _blogCommentsRepository.Create("test-blog-comments", comments);
        }
    }
}
