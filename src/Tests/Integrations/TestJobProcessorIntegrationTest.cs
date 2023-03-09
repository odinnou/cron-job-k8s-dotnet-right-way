using FluentAssertions;
using MELT;
using Microsoft.Extensions.DependencyInjection;
using MockServerClientNet;
using MockServerClientNet.Verify;
using Runner.Core.UseCases;
using Tests.Configuration;
using Xunit;
using static MockServerClientNet.Model.HttpRequest;

namespace Tests.Integrations.RestAdapters.hc;

public class TestJobProcessorIntegrationTest : BaseIntegrationTest
{
    [Fact]
    public async Task Process_should_call_HC_and_log_True_health_check_when_test_endpoint_is_healthy()
    {
        // arrange
        using (ServiceProvider = BuildTestServiceProvider())
        {
            using (MockServerClient = await ResetAndInitExpectations(RoutesFlag.TestEndpointHealthyHc))
            {
                // act
                await ServiceProvider.GetServices<IJobProcessor>().ToDictionary(useCase => useCase.JobToProcess)[Runner.Core.Models.Job.TestJob].Execute();

                // assert
                await MockServerClient.VerifyAsync(Request().WithPath("/hc").WithMethod(HttpMethod.Get), VerificationTimes.Exactly(1));
                IEnumerable<string> logs = ServiceProvider.GetRequiredService<ISerilogTestLoggerSink>().LogEntries.Where(log => !string.IsNullOrWhiteSpace(log.Message)).Select(log => log.Message!);

                logs.Should().Contain(s => s.Contains($"Test endpoint is it Healthy : True", StringComparison.OrdinalIgnoreCase));
                logs.Should().Contain(s => s.Contains($"{Runner.Core.Models.Job.TestJob} ended", StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    [Fact]
    public async Task Process_should_call_HC_and_log_False_health_check_when_test_endpoint_is_degraded()
    {
        // arrange
        using (ServiceProvider = BuildTestServiceProvider())
        {
            using (MockServerClient = await ResetAndInitExpectations(RoutesFlag.TestEndpointDegradedHc))
            {
                // act
                await ServiceProvider.GetServices<IJobProcessor>().ToDictionary(useCase => useCase.JobToProcess)[Runner.Core.Models.Job.TestJob].Execute();

                // assert
                await MockServerClient.VerifyAsync(Request().WithPath("/hc").WithMethod(HttpMethod.Get), VerificationTimes.Exactly(1));
                IEnumerable<string> logs = ServiceProvider.GetRequiredService<ISerilogTestLoggerSink>().LogEntries.Where(log => !string.IsNullOrWhiteSpace(log.Message)).Select(log => log.Message!);

                logs.Should().Contain(s => s.Contains($"Test endpoint is it Healthy : False", StringComparison.OrdinalIgnoreCase));
                logs.Should().Contain(s => s.Contains($"{Runner.Core.Models.Job.TestJob} ended", StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
