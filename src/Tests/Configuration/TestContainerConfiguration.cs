using TestEnvironment.Docker;

namespace Tests.Configuration;

public class TestContainerConfiguration : IDisposable
{
    public IDockerEnvironment MockServer { get; private set; }

    public TestContainerConfiguration()
    {
        MockServer = new DockerEnvironmentBuilder().SetName("mockserver-test")
              .AddContainer(container => container with
              {
                  Name = "mockserver",
                  ImageName = "mockserver/mockserver",
                  Ports = new Dictionary<ushort, ushort> { { 1080, 1090 } }
              }).Build();

        // Up them
        MockServer.UpAsync().Wait();

        // MockServer take times to be up inside the container, we should wait before starting tests
        Thread.Sleep(5000);
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
            try
            {
                MockServer.DisposeAsync().AsTask().Wait();
            }
            catch
            {
                // Occurs sometimes on local dev, during debug
            }
        }
    }
}