using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using DataScrapingService.Config;
using Microsoft.Extensions.Configuration;

namespace DataScrapingService.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Scraper _scraper;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(Constants.ScrapeIntervalMinutes);

        public Worker(ILogger<Worker> logger, Scraper scraper)
        {
            _logger = logger;
            _scraper = scraper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    await _scraper.ScrapeAndTransformAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during scraping and transformation.");
                }

                _logger.LogInformation("Waiting for the next run...");
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
