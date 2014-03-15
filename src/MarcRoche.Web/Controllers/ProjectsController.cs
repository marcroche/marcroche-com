using System.Web.Mvc;

namespace MarcRoche.Web.Controllers
{
    [RoutePrefix("projects")]
    public class ProjectsController : Controller
    {
        [OutputCache(CacheProfile="ProjectsIndex")]
        public ActionResult Index()
        {
            ViewBag.Title = "Marc Roche - Projects";
            return View();
        }
	}
}