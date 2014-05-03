using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using MarcRoche.Domain.Blog;
using MarcRoche.Services.Contracts;

namespace MarcRoche.Web.Controllers
{
    [RoutePrefix("blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private DateTimeFormatInfo _dateTimeFormatInfo = new DateTimeFormatInfo();

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [OutputCache(CacheProfile="BlogIndex")]
        public ActionResult Index()
        {
            ViewBag.Title = "Marc Roche - Musings on Software Development";
            return View(_blogService.GetLatestPost());
        }

        [OutputCache(CacheProfile = "BlogByTitle")]
        [Route("{year}/{month}/{title?}", Name = "BlogByTitle")]
        public ActionResult BlogByTitle(string year, string month, string title)
        {
            BlogPost post = _blogService.GetPostByTitle(title.Replace("-", " "));
            ViewBag.Title = "Marc Roche - " + post.Title;
            if (post == null)
            {
                return View(new BlogPost
                {
                    Title = "Ooops. This should't be here. Run away!",
                    Content = "<p></p>",
                    PublishDate = DateTime.UtcNow
                });
            }
            return View(post);
        }

        [OutputCache(CacheProfile = "BlogByYearAndMonth")]
        [Route("{year}/{month}", Name = "BlogByYearAndMonth")]
        public ActionResult Archive(string year, string month)
        {
            ViewBag.Title = "Marc Roche - " + _dateTimeFormatInfo.GetMonthName(int.Parse(month)) + year;
            return View(_blogService.GetArchive(int.Parse(year), int.Parse(month)));
        }
    }
}
