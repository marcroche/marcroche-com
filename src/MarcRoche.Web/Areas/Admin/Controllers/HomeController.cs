using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarcRoche.Domain.Services;

namespace MarcRoche.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAboutService _blogService;

        public HomeController(IAboutService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}