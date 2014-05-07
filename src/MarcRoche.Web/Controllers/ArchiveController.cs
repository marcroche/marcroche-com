using System.Web.Mvc;
using MarcRoche.Domain.Services;
using System.Linq;
using System.Globalization;
using MarcRoche.Web.Models;
using MarcRoche.Domain.Blog;
using System;

namespace MarcRoche.Web.Controllers
{
    [RoutePrefix("archive")]
    public class ArchiveController : Controller
    {
        private readonly IBlogService _blogService;

        public ArchiveController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [OutputCache(CacheProfile="ArchiveIndex")]
        public ActionResult Index()
        {
            ViewBag.Title = "Marc Roche - Archive";
            return View(_blogService.GetArchive());
        }

        [Route("{year}/{month}")]
        public ActionResult Archive(string year, string month)
        {
            return RedirectToActionPermanent("BlogByYearAndMonth", new { year = year, month = month });
        }

        [Route("{year}/{month}/{title?}")]
        public ActionResult Title(string year, string month, string title)
        {
            return RedirectToRoutePermanent("BlogByTitle", new { year = year, month = month, title = title });
        }
    }
}