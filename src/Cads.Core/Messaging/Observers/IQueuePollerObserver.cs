using Amazon.SQS.Model;

namespace Cads.Core.Messaging.Observers;

public interface IQueuePollerObserver<T>
{
    void OnMessageHandled(string messageId, DateTime handledAt, T payload, Message rawMessage);
    void OnMessageFailed(string messageId, DateTime failedAt, Exception exception, Message rawMessage);
}