namespace Runner.Core.Ports;

public interface ITestEndpointPort
{
    Task<bool> IsHealthy();
}
