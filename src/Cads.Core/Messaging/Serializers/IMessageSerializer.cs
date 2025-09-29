using Amazon.SQS.Model;

namespace Cads.Core.Messaging.Serializers;

public interface IMessageSerializer<out T>
{
    T? Deserialize(Message message);
}