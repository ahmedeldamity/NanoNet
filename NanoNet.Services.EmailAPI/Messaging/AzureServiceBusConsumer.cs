using Azure.Messaging.ServiceBus;
using NanoNet.Services.EmailAPI.Dtos;
using NanoNet.Services.EmailAPI.Interfaces;
using NanoNet.Services.EmailAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace NanoNet.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _emailCartQueueName;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private ServiceBusProcessor _emailCartProcessor;
        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _emailService = emailService;
            _configuration = configuration;
            _serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString")!;
            _emailCartQueueName = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue")!;

            var client = new ServiceBusClient(_serviceBusConnectionString);

            _emailCartProcessor = client.CreateProcessor(_emailCartQueueName);
        }

        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += ProcessEmailCartMessage;
            _emailCartProcessor.ProcessErrorAsync += ProcessEmailCartException;
            await _emailCartProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();
        }

        private async Task ProcessEmailCartMessage(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            var objMessage = JsonConvert.DeserializeObject<CartDto>(body);

            try
            {
                await _emailService.EmailCartAndLog(objMessage!);
                Console.WriteLine($"Sending email to {objMessage!.CartHeader.Email} for cart {objMessage.CartHeader.Id}");
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Task ProcessEmailCartException(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
