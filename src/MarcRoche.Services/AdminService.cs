using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Domain.Blog;
using MarcRoche.Domain.Services;

namespace MarcRoche.Services
{
    public class AdminService : IAdminService
    {
        private readonly IBlogService _blogService;

        private string xmlPath = @"C:\Development\MarcRoche_com\Main\MarcRoche.Web\App_Data\FileData\xml\";
        private string jsonPath = @"C:\Development\MarcRoche_com\Main\MarcRoche.Web\App_Data\blog\";
        private string markdownPath = @"C:\Development\MarcRoche_com\Main\MarcRoche.Web\App_Data\FileData\markdown\";

        public AdminService(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IEnumerable<string> GetBlogPostTitles()
        {
            return _blogService.GetAll().Select(x => x.Title);
        }

        public BlogPost GetBlogPost(string title)
        {
            BlogPost post = _blogService.GetPostByTitle(title.Replace("-", " "));
            post.Content = File.ReadAllText(Path.Combine(markdownPath, post.Title.ToLower().Replace(" ", "-") + ".md"));
            return post;
        }
    }
}
