using System;
using System.Collections.Generic;
using MarcRoche.Domain.Blog;
using MarcRoche.Domain.Blog.Archive;

namespace MarcRoche.Domain.Services
{
    public interface IBlogService
    {
        IDictionary<int, IList<ArchiveItem>> GetArchive();
        IEnumerable<BlogPost> Search(string searchText);
        BlogPost GetPostByTitle(string title);
        BlogPost GetLatestPost();
        IEnumerable<BlogPost> GetAll();

        void CreateComment(string title, BlogComment blogComment);
        IList<BlogComment> GetComments(string title);
    }
}
