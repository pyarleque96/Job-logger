using BTX.SERVICES.Options.Logger;
using Microsoft.Extensions.Configuration;

namespace BTX.XTEST.Helpers.Shared
{
    public static class BindHelpers
    {
        public static LoggerOptions FillLoggerOptions()
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.test.json")
                            .Build();

            var lc = config.GetSection("Logging");

            var loggerOptions = new LoggerOptions
            {
                logError = bool.Parse(lc["logError"]),
                logMessage = bool.Parse(lc["logMessage"]),
                logWarning = bool.Parse(lc["logWarning"]),
                logToConsole = bool.Parse(lc["logToConsole"]),
                logToDatabase = bool.Parse(lc["logToDatabase"]),
                logToFile = bool.Parse(lc["logToFile"]),
            };

            return loggerOptions;
        }
    }
}
