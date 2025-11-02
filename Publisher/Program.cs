using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceBusConsumer;

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("D:/2025/PracticeRepos/AzurePractice/AzurePractice.Common/app.settings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        //services.Configure<ServiceBusOptions>(context.Configuration.GetSection("ServiceBusOptions"));
        services.AddSingleton(context.Configuration);
        services.AddTransient<App>();
    })
    .Build();
var app = host.Services.GetRequiredService<App>();
await app.RunAsync();