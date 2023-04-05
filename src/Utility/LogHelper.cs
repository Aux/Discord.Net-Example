using Discord;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Example
{
    public static class LogHelper
    {
        public static Task OnLogAsync(ILogger logger, LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Verbose:
                    logger.LogInformation(msg.ToString());
                    break;

                case LogSeverity.Info:
                    logger.LogInformation(msg.ToString());
                    break;

                case LogSeverity.Warning:
                    logger.LogWarning(msg.ToString());
                    break;

                case LogSeverity.Error:
                    logger.LogError(msg.ToString());
                    break;

                case LogSeverity.Critical:
                    logger.LogCritical(msg.ToString());
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
