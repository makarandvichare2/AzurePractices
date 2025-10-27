using System;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
namespace AzureFunctions;

public class QueueTriggerFunction
{
    private readonly ILogger<QueueTriggerFunction> _logger;

    public QueueTriggerFunction(ILogger<QueueTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueTriggerFunction))]
    public void Run([QueueTrigger("mak-queue1", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}