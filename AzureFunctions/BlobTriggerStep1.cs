using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class BlobTriggerStep1
{
    private readonly ILogger<BlobTriggerStep1> _logger;

    public BlobTriggerStep1(ILogger<BlobTriggerStep1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobTriggerStep1))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}