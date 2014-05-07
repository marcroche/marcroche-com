using System.Collections.Generic;
using System.Web.Http;
using MarcRoche.Domain.Blog;
using MarcRoche.Domain.Services;
using MarcRoche.Web.Models;

namespace MarcRoche.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET api/comments
        //[AcceptVerbs("GET")]
        public IEnumerable<BlogComment> Get(string id)
        {
            return _commentService.Get(id);
        }

        // POST api/comments
        public void Post([FromBody]CommentsViewModel comment)
        {
            _commentService.Create(comment.Title, comment.Comment);
        }
    }
}
