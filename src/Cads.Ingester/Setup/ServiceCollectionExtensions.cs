using Cads.Application.Setup;
using Cads.Infrastructure.ApiClients.Setup;
using Cads.Infrastructure.Database.Setup;
using Cads.Infrastructure.Messaging.Setup;

namespace Cads.Ingester.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());

        services.ConfigureHealthChecks();

        services.AddApplicationLayer();

        services.AddDatabaseDependencies(configuration);

        services.AddMessagingDependencies(configuration);

        services.AddApiClientDependencies(configuration);
    }

    private static void ConfigureHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks();
    }
}