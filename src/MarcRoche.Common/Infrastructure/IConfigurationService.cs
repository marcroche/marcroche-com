using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcRoche.Common.Infrastructure
{
    public interface IConfigurationService
    {
        string GetApplicationSetting(string key);
    }
}
