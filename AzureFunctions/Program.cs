using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable(AzurePractice.Common.Constants.AZURE_STORAGE_CONNECTION);

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The AzureWebJobsStorage connection string is not configured.");
    }

    return new BlobServiceClient(connectionString);
});
builder.Services.AddSingleton(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable(AzurePractice.Common.Constants.AZURE_STORAGE_CONNECTION);

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The AzureWebJobsStorage connection string is not configured.");
    }

    return new TableServiceClient(connectionString);
});
builder.Services.AddSingleton(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable(AzurePractice.Common.Constants.SERVICEBUS_CONNECTION);

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The AzureBusConnection string is not configured.");
    }

    return new ServiceBusClient(connectionString);
});

builder.Build().Run();
