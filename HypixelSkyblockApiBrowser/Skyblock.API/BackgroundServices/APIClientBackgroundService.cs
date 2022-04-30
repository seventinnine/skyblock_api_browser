using Microsoft.Extensions.Hosting;
using Skyblock.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Skyblock.API.BackgroundServices
{
    public class APIClientBackgroundService : BackgroundService
    {
        private readonly APIClient apiClient;

        public APIClientBackgroundService(APIClient apiClient)
        {
            this.apiClient = apiClient;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = await apiClient.GetAHData();
            }
        }
    }
}
