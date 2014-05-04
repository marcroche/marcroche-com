using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcRoche.Common.Infrastructure;
using Moq;

namespace MarcRoche.Repository.Mongo.Tests
{
    public static class Mocks
    {
        public static IConfigurationService ConfigurationService { get; private set; }

        static Mocks()
        {
            SetupConfigurationService();
        }

        //SETUP CODE
        //============================================================================================
        private static void SetupConfigurationService()
        {
            Mock<IConfigurationService> configurationService = new Mock<IConfigurationService>();
            configurationService.Setup(x => x.GetApplicationSetting("database")).Returns(() =>
                {
                    return "blog-test";
                });
            configurationService.Setup(x => x.GetApplicationSetting("mongoConnectionString")).Returns(() =>
            {
                return "mongodb://localhost";
            });

            ConfigurationService = configurationService.Object;
        }
    }
}
