using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

public class ConsumeQueueMessages
{
    private readonly IConfiguration _configuration;
    public ConsumeQueueMessages(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task Consume()
    {
        var client = new ServiceBusClient(_configuration.GetValue<string>(AzurePractice.Common.Constants.SERVICEBUS_CONNECTION));

        var processor = client.CreateProcessor(AzurePractice.Common.Constants.SERVICEBUS_QUEUE_1);
        try
        {
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();

            Console.ReadKey();
            Console.WriteLine("stopping");
            await processor.StartProcessingAsync();
            Console.WriteLine("stopped");
        }
        catch
        {

        }
        async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.ErrorSource);
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
