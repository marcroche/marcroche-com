using System.Collections.Generic;
using Kiwi.Markdown;
using MarcRoche.Domain.Blog;
using MarcRoche.Repository.FileSystem;
using MarcRoche.Services;
using MarcRoche.Services.Contracts;
using Ninject.Modules;
using Repository;

namespace MarcRoche.IoC
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogService>().To<BlogService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IRepository<BlogPost>>().To<FileRepository<BlogPost>>().WithConstructorArgument("path", Settings.BlogFileDbPath);
            Bind<IRepository<AboutMe>>().To<FileRepository<AboutMe>>().WithConstructorArgument("path", Settings.AboutFileDbPath);
            Bind<IRepository<IList<BlogComment>>>().To<FileRepository<IList<BlogComment>>>().WithConstructorArgument("path", Settings.CommentsFileDbPath);

            //Bind<IMarkdownService>().To<MarkdownService>().WithConstructorArgument("contentProvider", 
        }
    }
}
