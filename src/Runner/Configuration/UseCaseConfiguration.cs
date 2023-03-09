using Microsoft.Extensions.DependencyInjection;
using Runner.Core.UseCases;
using Runner.Core.UseCases.Jobs;

namespace Runner.Configuration;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IJobProcessor, TestJobProcessor>();

        return services;
    }
}
