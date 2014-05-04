using System.Web.Mvc;
using MarcRoche.Domain.Services;

namespace MarcRoche.Web.Controllers
{
    [RoutePrefix("about")]
    public class AboutController : Controller
    {
        private readonly IBlogService _blogService;

        public AboutController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [OutputCache(CacheProfile = "AboutMeIndex")]
        public ActionResult Index()
        {
            ViewBag.Title = "Marc Roche - About";
            return View(_blogService.GetAboutMe());
        }
    }
}
