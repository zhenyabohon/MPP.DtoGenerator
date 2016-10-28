using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGeneration
{
    public static class ConfigurationSettingsManager
    {
        public static int MaxThreads => int.Parse(ConfigurationManager.AppSettings["MaxThreads"]);

        public static string Namespace => ConfigurationManager.AppSettings["Namespace"];
    }
}
