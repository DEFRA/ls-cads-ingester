using Cads.Core.Messaging.Contracts;
using Cads.Core.Messaging.Contracts.V1;
using Cads.Core.Messaging.MessageHandlers;
using Cads.Core.Messaging.Serializers;

namespace Cads.Application.MessageHandlers;

public class PlaceholderMessageHandler(IUnwrappedMessageSerializer<PlaceholderMessage> serializer) : IMessageHandler<PlaceholderMessage>
{
    private readonly IUnwrappedMessageSerializer<PlaceholderMessage> _serializer = serializer;

    public async Task<MessageType> Handle(UnwrappedMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        var messagePayload = _serializer.Deserialize(message);

        // Do something with the messagePayload

        return await Task.FromResult(messagePayload!);
    }
}