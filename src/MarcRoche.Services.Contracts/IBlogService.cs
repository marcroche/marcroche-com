using System;
using System.Collections.Generic;
using MarcRoche.Model.Blog;

namespace MarcRoche.Services.Contracts
{
    public interface IBlogService
    {
        AboutMe GetAboutMe();
        IEnumerable<BlogPost> GetArchive(int year, int month);
        IEnumerable<BlogPost> Search(string searchText);
        BlogPost GetPostByTitle(string title);
        BlogPost GetLatestPost();
        IEnumerable<BlogPost> GetAll();

        void CreateComment(string title, BlogComment blogComment);
        IList<BlogComment> GetComments(string title);

        //void DeleteBlogPost(Guid blogId);
        //void SaveBlogPost(BlogPost blogPost);
        //void SaveBlogPost<TElement>(Guid id, TElement element);
    }
}
