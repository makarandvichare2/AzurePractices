using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
namespace AzureFunctions;

public class TimeTriggerFunction
{
    private readonly ILogger _logger;

    public TimeTriggerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TimeTriggerFunction>();
    }

    [Function("TimeTriggerFunction")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}