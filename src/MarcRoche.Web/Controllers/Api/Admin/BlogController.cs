using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MarcRoche.Model.Blog;
using MarcRoche.Services.Contracts;

namespace MarcRoche.Web.Controllers.Api.Admin
{
    public class BlogController : ApiController
    {
        private readonly IAdminService _adminService;

        public BlogController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET api/blog
        [Route("api/admin/blog/titles", Name = "AdminBlogTitles")]
        public IEnumerable<string> Get()
        {
            return _adminService.GetBlogPostTitles();
        }

        [HttpGet]
        [Route("api/admin/blog/{title}", Name = "AdminBlogByTitle")]
        public BlogPost BlogByTitle(string title)
        {
            return _adminService.GetBlogPost(title);
        }
    }
}
