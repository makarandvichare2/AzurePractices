using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

public class ConsumeTopicMessages
{
    private readonly IConfiguration _configuration;
    public ConsumeTopicMessages(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task Consume(string subscriptionName)
    {
        var client = new ServiceBusClient(_configuration.GetValue<string>(AzurePractice.Common.Constants.SERVICEBUS_CONNECTION));

        var processor = client.CreateProcessor(AzurePractice.Common.Constants.SERVICEBUS_TOPIC_1, subscriptionName);
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
        catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
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
