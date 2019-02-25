using Microsoft.Extensions.Configuration;
using System.IO;


namespace Core
{
    public class ConfigHelper
    {
        public static IConfigurationRoot Configuration { get; set; }
        /// <summary>
        /// 根据节点读取值
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string ReadConfigByName(string name, string nodeName = "appSettings:")
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration[nodeName + name];
        }
    }
}
