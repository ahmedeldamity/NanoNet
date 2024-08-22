namespace NanoNet.MessageBus;
public interface IMessageBusService
{
    Task PublishMessage(string topic_queue_Name, object message);
}