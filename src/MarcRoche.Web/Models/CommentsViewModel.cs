using MarcRoche.Model.Blog;

namespace MarcRoche.Web.Models
{
    public class CommentsViewModel
    {
        public string Title { get; set; }
        public BlogComment Comment { get; set; }
    }
}