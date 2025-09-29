using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Contrib.HttpClient;
using System.Net;

namespace Cads.Ingester.Tests.Component.Endpoints;

public class HealthcheckEndpointTests(AppTestFixture appTestFixture) : IClassFixture<AppTestFixture>
{
    private readonly AppTestFixture _appTestFixture = appTestFixture;

    private const string CadsApiHealthEndpoint = $"{TestConstants.CadsApiBaseUrl}{TestConstants.HealthCheckEndpoint}";

    [Fact]
    public async Task GivenValidHealthCheckRequest_ShouldSucceed()
    {
        _appTestFixture.CadsApiClientHttpMessageHandlerMock.Reset();
        SetupCadsApiHealthCheckRequest(CadsApiHealthEndpoint, HttpStatusCode.OK);

        var client = _appTestFixture.AppWebApplicationFactory.Services
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("CadsApi");

        Console.WriteLine($"BaseAddress: {client.BaseAddress}");


        var response = await _appTestFixture.HttpClient.GetAsync("health");
        var responseBody = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        responseBody.Should().NotBeNullOrEmpty().And.Contain("\"status\": \"Healthy\"");
        responseBody.Should().NotContain("\"status\": \"Degraded\"");
        responseBody.Should().NotContain("\"status\": \"Unhealthy\"");

        VerifyCadsApiClientEndpointCalled(CadsApiHealthEndpoint, Times.Once());
    }

    private void VerifyCadsApiClientEndpointCalled(string requestUrl, Times times)
    {
        _appTestFixture.CadsApiClientHttpMessageHandlerMock.VerifyRequest(HttpMethod.Get, requestUrl, times);
    }

    private void SetupCadsApiHealthCheckRequest(string uri, HttpStatusCode httpStatusCode)
    {
        _appTestFixture.CadsApiClientHttpMessageHandlerMock.SetupRequest(HttpMethod.Get, uri)
            .ReturnsResponse(httpStatusCode);
    }
}