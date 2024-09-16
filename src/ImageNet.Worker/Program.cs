using ImageNet.Core.Interfaces;
using ImageNet.Core.Services;
using ImageNet.Infrastructure;
using ImageNet.Infrastructure.Data;
using ImageNet.Infrastructure.Services;
using ImageNet.Worker.Services;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);
        services.AddSingleton<IXmlDownloader, XmlDownloader>();
        services.AddSingleton<IXmlParser, XmlParser>();
        services.AddSingleton<IXmlProcessingService, XmlProcessingService>();

        services.AddHttpClient();
        services.AddHostedService<OneTimeWorker>();
    });

var host = builder.Build();

// Ensure database is created
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ImageNetDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while recreating the database.");
    }
}

await host.RunAsync();
