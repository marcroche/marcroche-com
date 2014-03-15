using System.Collections.Generic;
using System.Web.Http;
using MarcRoche.Model.Blog;
using MarcRoche.Services.Contracts;
using MarcRoche.Web.Models;

namespace MarcRoche.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IBlogService _blogService;

        public CommentsController(IBlogService blogService)
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
