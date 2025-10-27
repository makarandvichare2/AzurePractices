using Azure.Data.Tables;
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
    var connectionString = Environment.GetEnvironmentVariable(AzurePractice.Common.Constants.AZURE_STORQGE_CONNECTION);

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The AzureWebJobsStorage connection string is not configured.");
    }

    return new TableServiceClient(connectionString);
});

builder.Build().Run();
