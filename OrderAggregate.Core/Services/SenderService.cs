using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Services
{
    /// <summary>
    /// background task for periodic sending aggregated data to external system
    /// </summary>
    public class SenderService : IHostedService
    {
        private readonly ILogger<SenderService> _logger;
        private readonly OrderAggregatorService _orderAggregatorService;
        private readonly ConfigurationService _configurationService;

        private bool _stopRequested;

        public SenderService(ILogger<SenderService> logger, OrderAggregatorService orderAggregatorService, 
            ConfigurationService configurationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderAggregatorService = orderAggregatorService ?? throw new ArgumentNullException(nameof(orderAggregatorService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start");
            _ = Task.Run(async () =>
            {
                while (!_stopRequested)
                {
                    _logger.LogTrace("SendOrders");
                    try
                    {
                        SendOrders();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "SendOrders failed");
                    }
                    await Task.Delay(_configurationService.SenderService.IntervalSec * 1000);
                }
            });
            await Task.Yield();
        }

        private void SendOrders()
        {
            _logger.LogTrace("SendOrders");
            Console.WriteLine(JsonSerializer.Serialize(_orderAggregatorService.GetAggregatedOrders(), 
                new JsonSerializerOptions() { WriteIndented = true }));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            _stopRequested = true;
            await Task.Yield();
        }
    }
}
