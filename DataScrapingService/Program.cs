using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataScrapingService.Services;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<Scraper>();
                services.AddHostedService<Worker>();
            });
}
