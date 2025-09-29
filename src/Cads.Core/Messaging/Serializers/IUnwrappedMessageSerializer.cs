using Cads.Core.Messaging.Contracts;

namespace Cads.Core.Messaging.Serializers;

public interface IUnwrappedMessageSerializer<out T>
{
    T? Deserialize(UnwrappedMessage message);
}