namespace NanoNet.Services.EmailAPI.Interfaces;
public interface IAzureServiceBusConsumer
{
    Task Start();
    Task Stop();
}