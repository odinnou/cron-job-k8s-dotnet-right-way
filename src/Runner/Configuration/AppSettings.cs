using Runner.Core.Models;

#nullable disable warnings
namespace Runner.Configuration;

public class AppSettings
{
    public AppSettings() { }

    public AppSettings(IDictionary<string, object> environmentVariables)
    {
        JobToProcess = (Job)Enum.Parse(typeof(Job), (string)environmentVariables["JOB"], true);

        TestEndpointConfiguration = new EndpointConfiguration
        {
            BaseUrl = (string)environmentVariables["TEST_ENDPOINT_BASE_URL"]
        };
    }

    public Job JobToProcess { get; set; }
    public EndpointConfiguration TestEndpointConfiguration { get; set; }
}

public class EndpointConfiguration
{
    public string BaseUrl { get; set; }
}