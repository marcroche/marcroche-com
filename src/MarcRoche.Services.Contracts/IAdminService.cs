
using System.Collections.Generic;
using MarcRoche.Model.Blog;

namespace MarcRoche.Services.Contracts
{
    public interface IAdminService
    {
        IEnumerable<string> GetBlogPostTitles();
        BlogPost GetBlogPost(string title);
    }
}
