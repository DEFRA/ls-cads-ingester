using Moq;

namespace Cads.Ingester.Tests.Component;

public class AppTestFixture : IDisposable
{
    public readonly HttpClient HttpClient;
    public readonly AppWebApplicationFactory AppWebApplicationFactory;
    public readonly Mock<HttpMessageHandler> CadsApiClientHttpMessageHandlerMock;

    public AppTestFixture()
    {
        AppWebApplicationFactory = new AppWebApplicationFactory();
        HttpClient = AppWebApplicationFactory.CreateClient();
        CadsApiClientHttpMessageHandlerMock = AppWebApplicationFactory.CadsApiClientHttpMessageHandlerMock;
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
            AppWebApplicationFactory?.Dispose();
        }
    }
}