using Newtonsoft.Json;
using Skyblock.Common;
using Skyblock.Common.Domain;
using Skyblock.Common.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Client
{
    public delegate void ProgressChangedEvent(double completed);
    public class APIClient
    {
        private const string key = "?key=cf86f155-4dff-4a6a-ad08-2d927493ac0f";
        private static readonly int API_LIMIT_MS = 502;
        private readonly static HttpClient client = new();
        private bool useCached = false;

        public readonly AtomicReference<List<Auction>> CurrentData = new();

        public event ProgressChangedEvent? ProgressChanged;

        public APIClient()
        {
            try
            {
                string? auctionData = AppDataIO.TryReadText(Constants.OldAuctionsPath);
                if (auctionData is not null)
                {
                    var auctions = JsonConvert.DeserializeObject<List<Auction>>(auctionData)!;
                    CurrentData.Set(auctions);
                    useCached = true;
                }

            } catch (Exception ex)
            {
                Debug.WriteLine($"Could not access old auction data ({ex.StackTrace})");
            }
        }

        private async Task<CallResult?> GetCallResult(int pageNum)
        {
            var url = $"{Constants.APIEndpoint}{key}&page={pageNum}";
            HttpResponseMessage response = await client.GetAsync(url);

            string strResult = await response.Content.ReadAsStringAsync();
            CallResult? res = JsonConvert.DeserializeObject<CallResult>(strResult);
            if (res is not null && res.Success)
            {
                double percentage = res.Page / (double)res.TotalPages;
                ProgressChanged?.Invoke(percentage);
                await Task.Delay(API_LIMIT_MS);
            }
            return res;
        }

        public async Task<IList<Auction>> GetAHData()
        {
            if (useCached)
            {
                useCached = false;
                return CurrentData.Get();
            }

            List<AuctionData> res = new();
            int currPage = 0;
            CallResult? callRes;

            do
            {
                Debug.WriteLine($"Fetching page {currPage} ...");
                callRes = await GetCallResult(currPage++);
                if (callRes is not null && callRes.Success == true) res.AddRange(callRes.Auctions);

            } while (callRes is not null && callRes.Success == true);
            CurrentData.Set(res.ToDomains().ToList());

            var dataRef = CurrentData.Get();
            //await File.WriteAllTextAsync($"./data/{DateTime.Now:yyyy_mm_dd-hh_mm_ss}.json", JsonConvert.SerializeObject(dataRef));
            await AppDataIO.TryWriteTextAsync(Constants.OldAuctionsPath, JsonConvert.SerializeObject(dataRef));

            return dataRef;
        }

    }
}
