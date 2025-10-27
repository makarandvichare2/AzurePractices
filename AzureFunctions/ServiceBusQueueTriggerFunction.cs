using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

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
        [ServiceBusTrigger("queue.1", Connection = "AzureBusConnection")]
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