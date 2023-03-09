using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Runner.Configuration;
using Runner.Core.UseCases;
using Runner.DrivenAdapters.ThirdPartyAdapters.Configuration;
using Serilog;
using System.Collections;
namespace Runner;

public class Program
{
    private Program() { }

    public static async Task<int> Main()
    {
        AppSettings appSettings = BuildAppSettingsFromEnvironmentVariables();

        // Initialize serilog logger
        Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .MinimumLevel.Information()
             .Enrich.FromLogContext()
             .CreateLogger();

        // Setup the dependency injection
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton(appSettings)
            // Add some famous dependencies like AutoMapper
            .AddUseCases()
            .AddThirdParties()
            .AddLogging(cfg => cfg.AddSerilog())
            .BuildServiceProvider();

        try
        {
            await serviceProvider.GetServices<IJobProcessor>().ToDictionary(useCase => useCase.JobToProcess)[appSettings.JobToProcess].Execute();

            return 0;
        }
        catch (Exception exc)
        {
            serviceProvider.GetRequiredService<ILogger<Program>>().LogCritical(exc, "An error has occured during {Job} process.", appSettings.JobToProcess);

            return 1;
        }
    }

    private static AppSettings BuildAppSettingsFromEnvironmentVariables()
    {
        IDictionary<string, object> environmentVariables = Environment.GetEnvironmentVariables()
                                                                      .Cast<DictionaryEntry>()
                                                                      .Where(entry => entry.Key is string)
                                                                      .GroupBy(entry => ((string)entry.Key).ToUpper())
                                                                      .ToDictionary(g => g.Key, g => g.Single().Value!);

        return new AppSettings(environmentVariables);
    }
}