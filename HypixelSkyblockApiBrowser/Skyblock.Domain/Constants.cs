﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common
{
    public class Constants
    {
        public const string APIEndpoint = "https://api.hypixel.net/skyblock/auctions";
        public const string OldAuctionsPath = $"data/oldAuctions.json";
        public const double DoubleComparisonToleranceLow = 0.1;
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
