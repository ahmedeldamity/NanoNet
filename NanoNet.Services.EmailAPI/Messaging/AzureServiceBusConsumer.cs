using Azure.Messaging.ServiceBus;

namespace NanoNet.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _emailCartQueueName;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _emailCartProcessor;
        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString")!;
            _emailCartQueueName = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue")!;

            var client = new ServiceBusClient(_serviceBusConnectionString);

            _emailCartProcessor = client.CreateProcessor(_emailCartQueueName);
        }
    }
}
