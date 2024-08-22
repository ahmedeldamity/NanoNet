using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace NanoNet.MessageBus;
public class MessageBusService : IMessageBusService
{
    private readonly string _connectionString = "Endpoint=sb://nanonet.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=QO7TkqRrIgNuxvZTPlpWmfhUyjE4JZR6B+ASbDbwSr4=";

    public async Task PublishMessage(string topic_queue_Name, object message)
    {
        await using var client = new ServiceBusClient(_connectionString);

        var sender = client.CreateSender(topic_queue_Name);

        var jsonMessage = JsonConvert.SerializeObject(message);

        ServiceBusMessage serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await sender.SendMessageAsync(serviceBusMessage);
        await client.DisposeAsync();
    }
}