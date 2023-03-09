using RestSharp;
using Runner.Configuration;
using Runner.Core.Ports;

namespace Runner.DrivenAdapters.ThirdPartyAdapters;

public class TestEndpointAdapter : BaseThirdPartyAdapter, ITestEndpointPort
{
    public TestEndpointAdapter(AppSettings appSettings) : base(appSettings.TestEndpointConfiguration.BaseUrl)
    {
    }

    public async Task<bool> IsHealthy()
    {
        RestRequest request = new RestRequest("hc");

        return await ExecuteAsyncThrowingEvenForNotFound(request, Method.Get) == "Healthy";
    }
}
