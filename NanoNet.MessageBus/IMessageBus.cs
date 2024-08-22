namespace NanoNet.MessageBus;
public interface IMessageBus
{
    Task PublishMessage(string topic_queue_Name, object message);
}