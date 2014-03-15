using System;
using Kiwi.Markdown;
using Kiwi.Markdown.ContentProviders;

namespace MarcRoche.Services
{
    public class KiwiMarkdownService
    {
        private static readonly Lazy<IMarkdownService> Singleton;

        public static IMarkdownService Instance
        {
            get { return Singleton.Value; }
        }

        static KiwiMarkdownService()
        {
            Singleton = new Lazy<IMarkdownService>(
                () => new MarkdownService(new FileContentProvider("")), true);
        }
        //HttpContext.Current.Server.MapPath("~/App_Data/MarkdownFiles")
    }
}
