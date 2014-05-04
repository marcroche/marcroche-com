using System.Configuration;

namespace MarcRoche.Common.Infrastructure
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetApplicationSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];;
        }
    }
}
