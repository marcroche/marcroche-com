using System;
namespace MarcRoche.Model.Blog
{
    public class BlogComment
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string EmailHash { get; set; }
        public string HomePage { get; set; }
        public bool IsModerated { get; set; }
    }
}
