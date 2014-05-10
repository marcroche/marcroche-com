using MarcRoche.Repository.Mongo;
using MarcRoche.Repository.Mongo.Entities;
using Ninject.Modules;
using Repository;
using MarcRoche.Services;
using MarcRoche.Domain.Services;
using MarcRoche.Common.Infrastructure;
using MarcRoche.Domain.Blog;

namespace MarcRoche.IoC
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogService>().To<BlogService>();
            Bind<IAboutService>().To<AboutService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<IConfigurationService>().To<ConfigurationService>();

            Bind<IRepository<BlogPostEntity>>().To<MongoRepository<BlogPostEntity>>();
            Bind<IRepository<AboutEntity>>().To<MongoRepository<AboutEntity>>();
            Bind<IBlogRepository<BlogPostEntity>>().To<BlogRepository>();

            //Bind<IRepository<BlogPost>>().To<MongoRepository<BlogPost>>();
            //Bind<IRepository<About>>().To<MongoRepository<About>>();

            //Bind<IRepository<IList<BlogCommentEntity>>>().To<MongoRepository<IList<BlogCommentEntity>>>();
            //Bind<IMarkdownService>().To<MarkdownService>().WithConstructorArgument("contentProvider", 
        }
    }
}
