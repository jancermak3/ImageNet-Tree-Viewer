using ImageNet.Core.Interfaces;
using ImageNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OneTimeWorker : IHostedService
{
    private readonly ILogger<OneTimeWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IHostApplicationLifetime _appLifetime;

    public OneTimeWorker(
        ILogger<OneTimeWorker> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _appLifetime = appLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("One-time worker starting at: {time}", DateTimeOffset.Now);

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ImageNetDbContext>();
            var xmlDownloader = scope.ServiceProvider.GetRequiredService<IXmlDownloader>();
            var xmlProcessingService = scope.ServiceProvider.GetRequiredService<IXmlProcessingService>();

            if (!await dbContext.ImageNetItems.AnyAsync(cancellationToken))
            {
                _logger.LogInformation("ImageNetItems table is empty. Processing XML data.");

                try
                {
                    string xmlUrl = _configuration["XmlUrl"];
                    _logger.LogInformation("Downloading XML from {xmlUrl}", xmlUrl);
                    string xmlContent = await xmlDownloader.DownloadXmlAsync(xmlUrl);
                    _logger.LogInformation("Processing XML content.");
                    xmlProcessingService.ProcessXmlFile(xmlContent);

                    _logger.LogInformation("Data processing and saving completed.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during data processing.");
                }
                finally
                {
                    _logger.LogInformation("Stopping application.");
                    _appLifetime.StopApplication();
                }
            }
            else
            {
                _logger.LogInformation("Data already exists in the database. Skipping processing.");
                _appLifetime.StopApplication();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("One-time worker stopping at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}
