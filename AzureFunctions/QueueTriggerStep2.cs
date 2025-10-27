using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class QueueTriggerStep2
{
    private readonly ILogger<QueueTriggerStep2> _logger;

    public QueueTriggerStep2(ILogger<QueueTriggerStep2> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueTriggerStep2))]
    public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}