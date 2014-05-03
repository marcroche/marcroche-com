using System;
using System.IO;
using System.Xml.Linq;
using MarcRoche.FileRepository.Tests.Helpers;
using MarcRoche.Domain.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MarcRoche.FileRepository.Tests
{
    [TestClass]
    public class FormatConversions
    {
        [TestMethod]
        public void XmlToJson()
        {
            string xmlPath = @"..\..\..\MarcRoche.Web\App_Data\FileData\xml\";
            string jsonPath = @"..\..\..\MarcRoche.Web\App_Data\blog\";
            string markdownPath = @"..\..\..\MarcRoche.Web\App_Data\FileData\markdown\";

            foreach (string file in Directory.EnumerateFiles(xmlPath, "*.xml"))
            {
                FileInfo fi = new FileInfo(file);
                string fileName = fi.Name;
                string contents = File.ReadAllText(file);

                BlogPost post = (BlogPost)XElement.Parse(contents, LoadOptions.PreserveWhitespace);
                post.Content = File.ReadAllText(Path.Combine(markdownPath, post.Title.ToLower().Replace(" ", "-") + ".md"));
                post.HtmlContent = KiwiMarkdownService.Instance.ToHtml(post.Content);

                File.WriteAllText(Path.Combine(jsonPath, post.Title.ToLower().Replace(" ", "-") + ".json"),
                    JsonConvert.SerializeObject(post));
            }
        }
    }
}
