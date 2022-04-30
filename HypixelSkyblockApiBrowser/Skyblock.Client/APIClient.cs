using Newtonsoft.Json;
using Skyblock.Common.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Client
{
    public delegate void ProgressChangedEvent(double completed);
    public class APIClient
    {
        private const string requestURI = "https://api.hypixel.net/skyblock/auctions";
        private const string key = "?key=cf86f155-4dff-4a6a-ad08-2d927493ac0f";
        private const string requestParams = "&page=";
        private static readonly int API_LIMIT_MS = 502;
        private readonly static HttpClient client = new();

        public event ProgressChangedEvent ProgressChanged;

        private async Task<CallResult> GetCallResult(int pageNum)
        {
            var url = requestURI + key + requestParams + pageNum;
            HttpResponseMessage response = await client.GetAsync(url);
            string strResult = await response.Content.ReadAsStringAsync();
            CallResult res = JsonConvert.DeserializeObject<CallResult>(strResult);
            if (res.Success)
            {
                double percentage = res.Page / (double)res.TotalPages;
                ProgressChanged?.Invoke(percentage);
                await Task.Delay(API_LIMIT_MS);
            }
            return res;
        }

        public async Task<IList<Auction>> GetAHData()
        {
            List<AuctionData> res = new();
            int currPage = 0;
            CallResult callRes;

            do
            {
                callRes = await GetCallResult(currPage++);
                if (callRes.Success == true) res = res.Concat(callRes.Auctions).ToList();

            } while (callRes.Success == true);

            return res.ToDomains();
        }

    }
}
