using RestSharp;

namespace Runner.DrivenAdapters.ThirdPartyAdapters;

public abstract class BaseThirdPartyAdapter : IDisposable
{
    protected readonly RestClient _client;

    protected BaseThirdPartyAdapter(string baseUrl)
    {
        _client = new RestClient(new RestClientOptions(baseUrl) { ThrowOnAnyError = true });
    }

    protected async Task<string> ExecuteAsyncThrowingEvenForNotFound(RestRequest request, Method method)
    {
        RestResponse resp = await _client.ExecuteAsync(request, method);

        if (!resp.IsSuccessStatusCode || resp.ErrorException is not null)
        {
            throw resp.ErrorException ?? new InvalidOperationException($"Not a success HTTP StatusCode: {resp.StatusCode}");
        }

        return resp.Content!;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client?.Dispose();
        }
    }
}
