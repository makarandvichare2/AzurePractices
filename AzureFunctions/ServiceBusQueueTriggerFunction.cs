using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class ServiceBusQueueTriggerFunction
{
    private readonly ILogger<ServiceBusQueueTriggerFunction> _logger;

    public ServiceBusQueueTriggerFunction(ILogger<ServiceBusQueueTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ServiceBusQueueTriggerFunction))]
    public async Task Run(
        [ServiceBusTrigger(AzurePractice.Common.Constants.SERVICEBUS_QUEUE_1, Connection = AzurePractice.Common.Constants.SERVICEBUS_CONNECTION)]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}