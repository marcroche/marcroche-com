using System.Collections.Generic;
using System.Web.Http;
using MarcRoche.Domain.Blog;
using MarcRoche.Domain.Services;
using MarcRoche.Web.Models;

namespace MarcRoche.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IAboutService _blogService;

        public CommentsController(IAboutService blogService)
        {
            _blogService = blogService;
        }

        // GET api/comments
        //[AcceptVerbs("GET")]
        public IEnumerable<BlogComment> Get(string id)
        {
            return _blogService.GetComments(id);
        }

        // POST api/comments
        public void Post([FromBody]CommentsViewModel comment)
        {
            _blogService.CreateComment(comment.Title, comment.Comment);
        }
    }
}
