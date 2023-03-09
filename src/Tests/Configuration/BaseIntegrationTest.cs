using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MockServerClientNet;
using Runner;
using Runner.Configuration;
using Runner.DrivenAdapters.ThirdPartyAdapters.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using Tests.Fixtures;
using Xunit;

#nullable disable warnings
namespace Tests.Configuration;

/// <summary>
/// All integration tests will be played sequentially (not parallelized) thanks to this collection name: avoiding unique constraint issues.
/// </summary>
[Collection("INTEGRATION_TEST_COLLECTION")]
public abstract class BaseIntegrationTest
{
    public static readonly LoggerProviderCollection Providers = new LoggerProviderCollection();

    protected ServiceProvider ServiceProvider { get; set; }
    protected MockServerClient MockServerClient { get; set; }

    protected BaseIntegrationTest()
    {
    }

    protected static async Task<MockServerClient> ResetAndInitExpectations(RoutesFlag routesFlag)
    {
        MockServerClient mockServerClient = new("localhost", 1090);

        // reset expectations
        await mockServerClient.ResetAsync();

        // init expectations
        await RoutesExpectation.Configure(mockServerClient, routesFlag);

        return mockServerClient;
    }

    protected AppSettings DefaultAppSettings
    {
        get
        {
            return new AppSettings { TestEndpointConfiguration = new EndpointConfiguration { BaseUrl = "http://localhost:1090" } };
        }
    }

    protected ServiceProvider BuildTestServiceProvider(AppSettings? appSettings = null)
    {
        appSettings ??= DefaultAppSettings;

        // Initialize serilog logger
        Log.Logger = new LoggerConfiguration()
                 .WriteTo.Providers(Providers)
                 .MinimumLevel.Verbose()
                 .Enrich.FromLogContext()
                 .CreateLogger();

        // Setup the dependency injection
        return new ServiceCollection()
            .AddSingleton(appSettings)
            .AddUseCases()
            .AddThirdParties()
            .AddLogging(cfg => cfg.AddSerilogTest(options => options.FilterByNamespace(typeof(Program).Assembly.GetName().Name!)))
            .BuildServiceProvider();
    }
}

[Flags]
public enum RoutesFlag
{
    TestEndpointHealthyHc = 1,
    TestEndpointDegradedHc = 2
}
