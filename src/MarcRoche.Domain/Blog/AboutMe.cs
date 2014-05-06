using System;

namespace MarcRoche.Domain.Blog
{
    public class About
    {
        public static readonly Guid CacheKey = Guid.Parse("2471ecc5-256e-4ea9-accf-d74a652015d4");
        public string Content { get; set; }
        public string HtmlContent { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public Guid AboutId { get; set; }
    }
}
