using Azure;
using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;
namespace AzureFunctions;

public class BlobTriggerFunction
{
    private readonly ILogger<BlobTriggerFunction> _logger;
    private readonly TableServiceClient _tableServiceClient;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ServiceBusClient _serviceBusClient;
    public BlobTriggerFunction(ILogger<BlobTriggerFunction> logger, TableServiceClient tableServiceClient,
        BlobServiceClient blobServiceClient, ServiceBusClient serviceBusClient)
    {
        _logger = logger;
        _tableServiceClient = tableServiceClient;
        _blobServiceClient = blobServiceClient;
        _serviceBusClient = serviceBusClient;
    }

    [Function(nameof(BlobTriggerFunction))]
    [TableOutput("maktable1", Connection = AzurePractice.Common.Constants.AZURE_STORAGE_CONNECTION)]
    public async Task<MyTable> Run([BlobTrigger("mak-blob1/{name}", Connection = AzurePractice.Common.Constants.AZURE_STORAGE_CONNECTION)] Stream stream,
                          string name)
    {
        // add to blob
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("mak-blob2");

        await containerClient.UploadBlobAsync(name, stream);

        // add to service bus
        var sender = _serviceBusClient.CreateSender(AzurePractice.Common.Constants.SERVICEBUS_QUEUE_1);
        ServiceBusMessage message = new(Encoding.UTF8.GetBytes(name));
        await sender.SendMessageAsync(message);

        // add to table storage
        TableClient tableClient = _tableServiceClient.GetTableClient("maktable1");
        AsyncPageable<MyTable> queryResults = tableClient.QueryAsync<MyTable>();

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