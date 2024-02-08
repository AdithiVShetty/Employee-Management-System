using System.Configuration;

namespace Configuration
{
    public class AppConfig
    {
        public static string FileName()
        {
            return ConfigurationManager.AppSettings["filePath"];
        }
    }
}