using Newtonsoft.Json;
using Skyblock.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common
{
    public class AppSettings
    {
        static AppSettings()
        {
            string? apiKeyData = AppDataIO.TryReadText(Constants.AppSettingsPath);
            if (apiKeyData is null)
            {
                throw new ApplicationException($"Could not parse contents of {Constants.AppSettingsPath}.");
            }
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(apiKeyData);
            if (appSettings is null)
            {
                throw new ApplicationException($"Could not deserialize contents of {Constants.AppSettingsPath}.");
            }
            Instance = appSettings;
        }
        public static AppSettings Instance = new();
        public string APIEndpoint = "";
        public string APIKey = "";
        public double DoubleComparisonToleranceLow = 0.0d;
    }
}
