using System;
using System.Configuration;
using System.IO;

namespace MarcRoche.Repository.FileSystem
{
    internal class Settings
    {
        public static string BlogFileDbPath
        {
            get
            {
                string appData = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                return Path.Combine(appData, ConfigurationManager.AppSettings["BlogFileDbPath"]);
            }
        }

        public static string AboutFileDbPath
        {
            get
            {
                string appData = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                return Path.Combine(appData, ConfigurationManager.AppSettings["AboutFileDbPath"]);
            }
        }

        public static string CommentsFileDbPath
        {
            get
            {
                string appData = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                return Path.Combine(appData, ConfigurationManager.AppSettings["CommentsFileDbPath"]);
            }
        }
    }
}
