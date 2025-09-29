using Microsoft.AspNetCore.Builder;

namespace Cads.Ingester.Tests.Component.Config;

public class EnvironmentTest
{
    [Fact]
    public void IsNotDevModeByDefault()
    {
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
        var isDev = Cads.Ingester.Config.Environment.IsDevMode(builder);
        Assert.False(isDev);
    }
}