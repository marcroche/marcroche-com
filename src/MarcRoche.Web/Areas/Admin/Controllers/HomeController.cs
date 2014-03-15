using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarcRoche.Services.Contracts;

namespace MarcRoche.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}