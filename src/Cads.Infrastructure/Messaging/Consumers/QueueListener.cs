using Cads.Core.Messaging.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cads.Infrastructure.Messaging.Consumers;

public class QueueListener(IQueuePoller queuePoller, ILogger<QueueListener> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("QueueListener start requested.");

        return queuePoller.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("QueueListener stop requested.");

        try
        {
            await queuePoller.StopAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // Swallow expected cancellation
        }
    }
}