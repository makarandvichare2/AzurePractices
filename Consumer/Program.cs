using Azure.Messaging.ServiceBus;

// the client that owns the connection and can be used to create senders and receivers
ServiceBusClient client;

// the sender used to publish messages to the queue
ServiceBusSender sender;

// number of messages to be sent to the queue
const int numOfMessages = 3;

client = new ServiceBusClient(
    "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;");
sender = client.CreateSender("queue.1");

var processor = client.CreateProcessor("queue.1");
try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;
    await processor.StartProcessingAsync();

    Console.WriteLine("wait");
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

Console.WriteLine("Press any key to end the application");
Console.ReadKey();