using System.Collections.Generic;
using Kiwi.Markdown;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Domain.Blog;
using MarcRoche.Repository.FileSystem;
using MarcRoche.Repository.Mongo;
using MarcRoche.Repository.Mongo.Entities;
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
            Bind<IConfigurationService>().To<ConfigurationService>();

            Bind<IRepository<BlogPostEntity>>().To<MongoRepository<BlogPostEntity>>();
            Bind<IRepository<AboutMeEntity>>().To<MongoRepository<AboutMeEntity>>();
            //Bind<IRepository<IList<BlogCommentEntity>>>().To<MongoRepository<IList<BlogCommentEntity>>>();
            //Bind<IMarkdownService>().To<MarkdownService>().WithConstructorArgument("contentProvider", 
        }
    }
}
