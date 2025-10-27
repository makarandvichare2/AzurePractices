using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using System.Text.Json;
namespace AzureFunctions;

public class SqlTriggerFunction
{
    private readonly ILogger _logger;

    public SqlTriggerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SqlTriggerFunction>();
    }

    // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
    [Function("SqlTriggerFunction")]
    public void Run(
        [SqlTrigger("[dbo].[ToDoItem]", AzurePractice.Common.Constants.SQL_CONNECTION)] IReadOnlyList<SqlChange<ToDoItem>> changes,
            FunctionContext context)
    {
        _logger.LogInformation("SQL Changes: " + JsonSerializer.Serialize(changes));

    }
}

public class ToDoItem
{
    public string Id { get; set; }
    public int Priority { get; set; }
    public string Description { get; set; }
}