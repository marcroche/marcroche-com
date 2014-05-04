
using System.Collections.Generic;
using MarcRoche.Domain.Blog;

namespace MarcRoche.Domain.Services
{
    public interface IAdminService
    {
        IEnumerable<string> GetBlogPostTitles();
        BlogPost GetBlogPost(string title);
    }
}
