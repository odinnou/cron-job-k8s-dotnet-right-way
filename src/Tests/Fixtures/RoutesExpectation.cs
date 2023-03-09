using MockServerClientNet;
using MockServerClientNet.Model;
using Tests.Configuration;
using static MockServerClientNet.Model.HttpRequest;
using static MockServerClientNet.Model.HttpResponse;

namespace Tests.Fixtures;

public static class RoutesExpectation
{
    public static async Task Configure(MockServerClient mockServerClient, RoutesFlag routesFlag)
    {
        if (routesFlag.HasFlag(RoutesFlag.TestEndpointHealthyHc))
        {
            await mockServerClient.When(Request()
                .WithMethod(HttpMethod.Get)
                .WithPath($"/hc"), Times.Unlimited()
            )
        .RespondAsync(Response()
            .WithStatusCode(System.Net.HttpStatusCode.OK)
            .WithHeader("Content-Type", "text/plain; charset=utf-8")
            .WithBody("Healthy")
        );
        }
        else if (routesFlag.HasFlag(RoutesFlag.TestEndpointDegradedHc))
        {
            await mockServerClient.When(Request()
               .WithMethod(HttpMethod.Get)
               .WithPath($"/hc"), Times.Unlimited()
           )
       .RespondAsync(Response()
           .WithStatusCode(System.Net.HttpStatusCode.OK)
           .WithHeader("Content-Type", "text/plain; charset=utf-8")
           .WithBody("Degraded")
       );
        }
    }
}