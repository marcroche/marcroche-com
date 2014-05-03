using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace MarcRoche.Domain.Blog
{
    public class BlogPost
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public string HtmlContent { get; set; }
        public string MarkdownContent { get; set; }
        public Guid Id { get; set; }
        public DateTime PublishDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }
        public IEnumerable<BlogComment> Comments { get; private set; }
        public IEnumerable<string> ScriptDependencies { get; set; }

        public BlogPost()
        {
            Comments = new List<BlogComment>();
            ScriptDependencies = new List<string>();
            Tags = new List<string>();
        }

        public string SearchableContent
        {
            get
            {
                //TODO: Implementation is RegEx stripped Content
                // Should this be on the model??? Maybe it is part of the save process / service / repository

                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(HtmlContent);
                //var text = doc.DocumentNode.SelectNodes("//body//text()").Select(node => node.InnerText);
                //StringBuilder output = new StringBuilder();
                //foreach (string line in text)
                //{
                //    output.AppendLine(line);
                //}
                //string textOnly = HttpUtility.HtmlDecode(output.ToString());

                return string.Empty;
            }
        }

        public static implicit operator XElement(BlogPost blogPost)
        {
            XElement scripts = new XElement("Scripts");
            blogPost.ScriptDependencies.ToList().ForEach(s =>
            {
                scripts.Add(new XElement("Script", new XCData(s)));
            });

            return new XElement("BlogPost",
                new XAttribute("title", blogPost.Title),
                new XAttribute("publishdate", blogPost.PublishDate.ToString()),
                new XAttribute("id", blogPost.Id.ToString()),
                new XAttribute("author", blogPost.Author ?? string.Empty),
                scripts,
                new XElement("Content", !string.IsNullOrEmpty(blogPost.Content) ? XElement.Parse(blogPost.Content) : null),
                new XElement("HtmlContent", !string.IsNullOrEmpty(blogPost.HtmlContent) ? XElement.Parse(blogPost.HtmlContent) : null));
        }

        public static explicit operator BlogPost(XElement xml)
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

            if (xml.Element("Scripts") != null)
            { 
                blogPost.ScriptDependencies = 
                    xml.Element("Scripts").Elements("Script").ToList().Select(x => x.Value);
            }

            return blogPost;
        }
    }
}
