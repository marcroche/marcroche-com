using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Domain.Blog;

namespace MarcRoche.Domain.Services
{
    public interface ICommentService
    {
        IEnumerable<BlogComment> Get(string id);
        bool Create(string id, BlogComment comment);
    }
}
