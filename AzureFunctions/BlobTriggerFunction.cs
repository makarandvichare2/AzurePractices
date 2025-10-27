using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
namespace AzureFunctions;

public class BlobTriggerFunction
{
    private readonly ILogger<BlobTriggerFunction> _logger;

    public BlobTriggerFunction(ILogger<BlobTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobTriggerFunction))]
    [TableOutput("maktable1", Connection = AzurePractice.Common.Constants.AZURE_STORQGE_CONNECTION)]
    public async Task<MyTable> Run([BlobTrigger("mak-blob1/{name}", Connection = AzurePractice.Common.Constants.AZURE_STORQGE_CONNECTION)] byte[] content,
                          string name)
    {
        return new MyTable
        {
            PartitionKey = "blobpartition",
            RowKey = Guid.NewGuid().ToString(),
            Name = name
        }; 
        //using var blobStreamReader = new StreamReader(stream);
        //var content = await blobStreamReader.ReadToEndAsync();
        //_logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}

public class MyTable
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Name { get; set; }
}