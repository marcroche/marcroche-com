using System.Web.Mvc;
using MarcRoche.Domain.Services;

namespace MarcRoche.Web.Controllers
{
    [RoutePrefix("about")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService blogService)
        {
            _aboutService = blogService;
        }

        [OutputCache(CacheProfile = "AboutIndex")]
        public ActionResult Index()
        {
            ViewBag.Title = "Marc Roche - About This Blog";
            return View(_aboutService.Get());
        }
    }
}
