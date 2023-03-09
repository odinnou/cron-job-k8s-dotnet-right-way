using Microsoft.Extensions.Logging;
using Runner.Core.Models;
using Runner.Core.Ports;

namespace Runner.Core.UseCases.Jobs;

public class TestJobProcessor : JobProcessorBase
{
    private readonly ITestEndpointPort _testEndpointPort;

    public TestJobProcessor(ITestEndpointPort testEndpointPort, ILogger<TestJobProcessor> logger) : base(logger)
    {
        _testEndpointPort = testEndpointPort;
    }

    public override Job JobToProcess => Job.TestJob;

    public override async Task Process()
    {
        // Just for en example ! Imagine a daily report processing
        bool isHealthy = await _testEndpointPort.IsHealthy();

        _logger.LogInformation("Test endpoint is it Healthy : {IsHealthy}", isHealthy);
    }
}
