using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
namespace AzureFunctions;

public class BlobTriggerStep2
{
    private readonly ILogger<BlobTriggerStep2> _logger;

    public BlobTriggerStep2(ILogger<BlobTriggerStep2> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobTriggerStep2))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}
