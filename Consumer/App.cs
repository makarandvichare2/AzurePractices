using Microsoft.Extensions.Configuration;

namespace ServiceBusConsumer
{
    public class App
    {
        private readonly IConfiguration _configuration;

        public App(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async Task RunAsync()
        {

            Console.WriteLine("Consumer Application");
            Console.WriteLine(new String('=', 100));
            Console.WriteLine();
            Console.WriteLine("Press 1 for queue and 2 for topic");
            Console.WriteLine();
            Console.SetWindowSize(50, 50);
            Console.ForegroundColor = ConsoleColor.Green;
            var requestType = Console.ReadLine();
            if (requestType == "1")
            {
                var request = new ConsumeQueueMessages(_configuration);
                Console.WriteLine(AzurePractice.Common.Constants.SERVICEBUS_QUEUE_1);
                Console.WriteLine(new String('=', 100));
                await request.Consume();
            }

            if (requestType == "2")
            {
                var request = new ConsumeTopicMessages(_configuration);
                Console.WriteLine(AzurePractice.Common.Constants.SERVICEBUS_TOPIC_1);
                Console.WriteLine(new String('=', 100));
                Console.WriteLine(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_1);
                await request.Consume(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_1);
                Console.WriteLine(new String('=', 100));
                Console.WriteLine(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_2);
                await request.Consume(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_2);
                Console.WriteLine(new String('=', 100));
                Console.WriteLine(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_3);
                await request.Consume(AzurePractice.Common.Constants.SERVICEBUS_SUBCRIPTION_3);

            }

            Console.WriteLine("Press any key to end the application");
            Console.ReadKey();
        }
    }
}
