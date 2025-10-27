using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
namespace AzureFunctions;

public class BlobTriggerFunction
{
    private readonly ILogger<BlobTriggerFunction> _logger;
    private readonly TableClient _tableClient;
    public BlobTriggerFunction(ILogger<BlobTriggerFunction> logger, TableClient tableClient)
    {
        _logger = logger;
        _tableClient = tableClient;
    }

    [Function(nameof(BlobTriggerFunction))]
    [TableOutput("maktable1", Connection = AzurePractice.Common.Constants.AZURE_STORQGE_CONNECTION)]
    public async Task<MyTable> Run([BlobTrigger("mak-blob1/{name}", Connection = AzurePractice.Common.Constants.AZURE_STORQGE_CONNECTION)] byte[] content,
                          string name)
    {
        AsyncPageable<MyTable> queryResults = _tableClient.QueryAsync<MyTable>();

        var count = await queryResults.CountAsync();
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

public class MyTable : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? Timestamp { get; set; } = null;
    public ETag ETag { get; set; } = default;
}