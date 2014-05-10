using System;
using System.Xml.Linq;
using MarcRoche.Domain.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace MarcRoche.Model.Tests
{
    [TestClass]
    public class BlogPostTests
    {
        [TestMethod]
        public void CanSerializeBlogPost()
        {
            XElement blogPost = (XElement)post;

            Assert.AreEqual(XElement.Parse(xml).ToString(SaveOptions.DisableFormatting), blogPost.ToString(SaveOptions.DisableFormatting));
        }

        [TestMethod]
        public void CanDeserializeBlogPost()
        {
            BlogPost blogPost = (BlogPost)XElement.Parse(xml);

            Assert.AreEqual("Path Finding with D3", blogPost.Title);
            Assert.AreEqual(12, blogPost.ScriptDependencies.Count());
        }

        private static string xml =
@"<BlogPost title=""Path Finding with D3"" publishdate=""2/15/2014 12:00:00 AM"" id=""63cb5e5b-345f-4760-a069-56cc16fac5e6"" author=""marcroche"">
    <Scripts>
        <Script>
            <![CDATA[<script src=""http://jashkenas.github.io/underscore/underscore-min.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""http://code.jquery.com/jquery-1.10.2.min.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""http://d3js.org/d3.v3.min.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""http://cdnjs.cloudflare.com/ajax/libs/q.js/0.9.2/q.min.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/boot.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/Edge.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/Vertex.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/GridGraph.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/MinPriorityQueue.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/Dijkstra.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/Grid.js""></script>]]>
        </Script>
        <Script>
            <![CDATA[<script src=""/artifacts/path-finding-with-d3/main.js""></script>]]>
        </Script>
    </Scripts>
    <Content />
    <HtmlContent />
</BlogPost>";

        private static BlogPost post = new BlogPost
        {
            Title = "Path Finding with D3",
            PublishDate = DateTime.Parse("2/15/2014 12:00:00 AM"),
            BlogPostId = Guid.Parse("63cb5e5b-345f-4760-a069-56cc16fac5e6"),
            Author = "marcroche",
            ScriptDependencies = new List<string>
            {
                "<script src=\"http://jashkenas.github.io/underscore/underscore-min.js\"></script>",
                "<script src=\"http://code.jquery.com/jquery-1.10.2.min.js\"></script>",
                "<script src=\"http://d3js.org/d3.v3.min.js\"></script>",
                "<script src=\"http://cdnjs.cloudflare.com/ajax/libs/q.js/0.9.2/q.min.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/boot.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/Edge.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/Vertex.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/GridGraph.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/MinPriorityQueue.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/Dijkstra.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/Grid.js\"></script>",
                "<script src=\"/artifacts/path-finding-with-d3/main.js\"></script>"
            }
        };
    }
}
