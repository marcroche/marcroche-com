
using System.Collections.Generic;
using MarcRoche.Domain.Blog;

namespace MarcRoche.Services.Contracts
{
    public interface IAdminService
    {
        IEnumerable<string> GetBlogPostTitles();
        BlogPost GetBlogPost(string title);
    }
}
