using Newtonsoft.Json;
using Skyblock.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common
{
    public class Constants
    {
        public const string OldAuctionsPath = $"data/oldAuctions.json";
        public const string AccessoriesPath = $"data/accessories.json";
        public const string AppSettingsPath = $"data/appSettings.json";
        public static readonly string NoStars = "None";
        public static readonly string[] StarOptions = new string[]
        {
            "None",
            "✪",
            "✪✪",
            "✪✪✪",
            "✪✪✪✪",
            "✪✪✪✪✪"
        };
    }
}
