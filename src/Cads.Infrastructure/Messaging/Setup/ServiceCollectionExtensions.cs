using Amazon.SQS;
using Cads.Application.MessageHandlers;
using Cads.Core.Messaging.Consumers;
using Cads.Core.Messaging.Contracts;
using Cads.Core.Messaging.Contracts.Serializers;
using Cads.Core.Messaging.Contracts.V1;
using Cads.Core.Messaging.Contracts.V1.Serializers;
using Cads.Core.Messaging.MessageHandlers;
using Cads.Core.Messaging.Serializers;
using Cads.Infrastructure.Messaging.Configuration;
using Cads.Infrastructure.Messaging.Consumers;
using Cads.Infrastructure.Messaging.MessageHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Infrastructure.Messaging.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddMessagingDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var intakeEventQueueConfig = configuration.GetRequiredSection($"{nameof(QueueConsumerOptions)}:{nameof(IntakeEventQueueOptions)}");
        services.Configure<IntakeEventQueueOptions>(intakeEventQueueConfig);

        var intakeEventQueueOptions = intakeEventQueueConfig.Get<IntakeEventQueueOptions>() ?? new() { QueueUrl = "Missing", Disabled = true };
        services.AddSingleton(intakeEventQueueOptions);

        if (configuration["LOCALSTACK_ENDPOINT"] != null)
        {
            services.AddSingleton<IAmazonSQS>(sp =>
            {
                var config = new AmazonSQSConfig
                {
                    ServiceURL = configuration["AWS:ServiceURL"],
                    AuthenticationRegion = configuration["AWS:Region"],
                    UseHttp = true
                };
                var credentials = new Amazon.Runtime.BasicAWSCredentials("test", "test");
                return new AmazonSQSClient(credentials, config);
            });
        }
        else
        {
            services.AddAWSService<IAmazonSQS>();
        }

        services.AddMessageConsumers();

        services.AdddMessageSerializers();

        services.AddMessageHandlers();

        if (!intakeEventQueueOptions.Disabled)
        {
            services.AddHealthChecks()
                .AddCheck<QueueHealthCheck<IntakeEventQueueOptions>>("intake-event-consumer", tags: ["aws", "sqs"]);
        }
    }

    private static void AddMessageConsumers(this IServiceCollection services)
    {
        services.AddHostedService<QueueListener>()
            .AddSingleton<IQueuePoller, QueuePoller>();
    }

    private static void AdddMessageSerializers(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSerializer<SnsEnvelope>, SnsEnvelopeSerializer>();

        services.AddSingleton<IUnwrappedMessageSerializer<PlaceholderMessage>, PlaceholderMessageSerializer>();
    }

    private static IServiceCollection AddMessageHandlers(this IServiceCollection services)
    {
        services.AddTransient<IMessageHandler<PlaceholderMessage>, PlaceholderMessageHandler>();

        var messageHandlerManager = new InMemoryMessageHandlerManager();
        messageHandlerManager.AddReceiver<PlaceholderMessage, IMessageHandler<PlaceholderMessage>>();

        services.AddSingleton<IMessageHandlerManager>(messageHandlerManager);

        return services;
    }
}