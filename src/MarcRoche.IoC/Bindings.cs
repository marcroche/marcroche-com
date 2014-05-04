using MarcRoche.Repository.Mongo;
using MarcRoche.Repository.Mongo.Entities;
using Ninject.Modules;
using Repository;
using MarcRoche.Services;
using MarcRoche.Domain.Services;
using MarcRoche.Common.Infrastructure;

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
