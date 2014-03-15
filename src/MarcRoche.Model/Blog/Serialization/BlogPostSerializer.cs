using System;
using System.Xml.Linq;

namespace MarcRoche.Model.Blog.Serialization
{
    public class BlogPostSerializer
    {
        public XElement Serialize(BlogPost post)
        {
            return new XElement("BlogPost",
                new XAttribute("title", post.Title),
                new XAttribute("publishdate", post.PublishDate.ToString()),
                new XAttribute("id", post.Id.ToString()),
                new XAttribute("author", post.Author ?? string.Empty),
                new XElement("Content", XElement.Parse(post.Content)),
                new XElement("HtmlContent", XElement.Parse(post.HtmlContent)));

            //new XElement("Content", Convert.ToBase64String(Encoding.UTF8.GetBytes(post.Content), Base64FormattingOptions.InsertLineBreaks)));
        }

        public BlogPost Deserialize(XElement xml)
        {
            BlogPost blogPost = new BlogPost
            {
                Author = xml.Attribute("author").Value,
                Content = xml.Element("Content").Value,
                HtmlContent = xml.Element("HtmlContent") != null ? xml.Element("HtmlContent").Value : "",
                Id = Guid.Parse(xml.Attribute("id").Value),
                PublishDate = DateTime.Parse(xml.Attribute("publishdate").Value),
                Title = xml.Attribute("title").Value
            };

            return blogPost;
        }
    }
}