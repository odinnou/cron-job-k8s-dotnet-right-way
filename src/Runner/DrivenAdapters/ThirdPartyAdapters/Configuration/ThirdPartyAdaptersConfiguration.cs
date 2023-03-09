using Microsoft.Extensions.DependencyInjection;
using Runner.Core.Ports;

namespace Runner.DrivenAdapters.ThirdPartyAdapters.Configuration;

public static class ThirdPartyConfiguration
{
    public static IServiceCollection AddThirdParties(this IServiceCollection services)
    {
        services.AddSingleton<ITestEndpointPort, TestEndpointAdapter>();

        return services;
    }
}
