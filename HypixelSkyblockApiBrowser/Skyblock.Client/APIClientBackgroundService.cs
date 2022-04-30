using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skyblock.Client
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
