Console.WriteLine("Publisher Application");
Console.WriteLine(new String('=', 100));
Console.WriteLine();
Console.WriteLine("Press 1 for queue and 2 for topic");
Console.WriteLine();
Console.SetWindowSize(50, 50);
Console.ForegroundColor = ConsoleColor.Yellow;
var requestType = Console.ReadLine();
if(requestType == "1")
{
    var request = new PublishQueueMessage();
    await request.Publish();
}

if (requestType == "2")
{
    var request = new PublishTopicMessage();
    await request.Publish();
}

Console.WriteLine("Press any key to end the application");
Console.ReadKey();