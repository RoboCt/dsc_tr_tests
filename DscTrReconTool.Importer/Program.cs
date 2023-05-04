
using DscTrReconTool.Importer;
using DscTrReconTool.Importer.Common;
using DscTrReconTool.Importer.Common.Options;
using DscTrReconTool.Importer.Importers;
using DscTrReconTool.Importer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static DscTrReconTool.Importer.Common.Options.CommandLineOptions;

var serviceProvider = ConfigureServicesProvider();

IApplicationRunner applicationRunner = serviceProvider.GetService<IApplicationRunner>()!;
return await applicationRunner!.RunAsync(args);

static IServiceProvider ConfigureServicesProvider()
{
    IServiceCollection serviceCollection = new ServiceCollection();

    IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
        .AddJsonFile("appsettings.json", false)
        .AddEnvironmentVariables()
        .Build();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    serviceCollection.AddSingleton(_configuration);

    serviceCollection.AddTransient<IApplicationRunner, ApplicationRunner>();
    serviceCollection.AddTransient<IConsole, ConsoleController>();
    serviceCollection.AddTransient<ICommandLineParser, CommandLineParser>();
    serviceCollection.AddTransient<IImporterApplicationFactory, ImporterApplicationFactory>();
    serviceCollection.AddTransient<IImporterFactory, ImporterFactory>();

    serviceCollection.AddTransient<IImporterApplication, FileImporterApplication<FileArgsOptions>>();

    return serviceCollection.BuildServiceProvider();
}
