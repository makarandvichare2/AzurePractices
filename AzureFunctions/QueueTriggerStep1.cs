using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class QueueTriggerStep1
{
    private readonly ILogger<QueueTriggerStep1> _logger;

    public QueueTriggerStep1(ILogger<QueueTriggerStep1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueTriggerStep1))]
    public void Run([QueueTrigger("mak-queue1", Connection = AzurePractice.Common.Constants.AZURE_STORAGE_CONNECTION)] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}