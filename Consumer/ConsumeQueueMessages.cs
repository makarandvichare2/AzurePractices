using Azure.Messaging.ServiceBus;

public class ConsumeQueueMessages
{
    public async Task Consume()
    {
        var client = new ServiceBusClient(AzurePractice.Common.Constants.SERVICEBUS_CONNECTION_STRING);

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
