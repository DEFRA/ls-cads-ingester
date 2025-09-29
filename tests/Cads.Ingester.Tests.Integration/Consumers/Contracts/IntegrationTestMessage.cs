using Cads.Core.Messaging.Contracts;

namespace Cads.Ingester.Tests.Integration.Consumers.Contracts;

public class IntegrationTestMessage : MessageType
{
    public string Message { get; init; } = string.Empty;
}