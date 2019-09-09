using BTX.SERVICES.Options.Logger;
using Microsoft.Extensions.Options;
using System;
using static BTX.SERVICES.Enums.Logger.LoggerEnums;
using static BTX.SERVICES.Helpers.Logger.LoggerHelpers;

namespace BTX.SERVICES.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly LoggerOptions _loggerOptions;

        public LoggerService(IOptions<LoggerOptions> loggerOptions)
        {
            _loggerOptions = loggerOptions.Value;
        }

        public void LogMessage(string message, MessageType mType) //~~~~~ existían 2 variables con el mismo nombre, se cambiaron los 3 bool por un Enum.
        {
            if (message == null || message.Length == 0) return;

            message = message.Trim(); //~~~~~~ se realizaba el Trim() de la cadena antes de validar si era nulo, además de producir una excepción no controlada y tampoco se almacenaba dicho valor modificado.

            if (!_loggerOptions.logToConsole && !_loggerOptions.logToFile && !_loggerOptions.logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if (!_loggerOptions.logError && !_loggerOptions.logMessage && !_loggerOptions.logWarning)
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            switch (mType)
            {
                case MessageType.Message:
                    LogMessage(message);
                    break;
                case MessageType.Error:
                    LogError(message);
                    break;
                case MessageType.Warning:
                    LogWarning(message);
                    break;
                default:
                    break;
            }

            return;

        }


        //~~~~~~~~~~~~~~~~~~~~~private methods


        private void LogError(string message)
        {
            if (!_loggerOptions.logError) return;

            SaveToOutPut(message, MessageType.Error);
        }

        private void LogWarning(string message)
        {
            if (!_loggerOptions.logWarning) return;

            SaveToOutPut(message, MessageType.Warning);
        }

        private void LogMessage(string message)
        {
            if (!_loggerOptions.logMessage) return;

            SaveToOutPut(message, MessageType.Message);
        }

        private void SaveToOutPut(string message, MessageType mType)
        {
            if (_loggerOptions.logToConsole) Methods.LogToConsole(message, mType);
            if (_loggerOptions.logToFile) Methods.LogToFileText(message, mType);
            if (_loggerOptions.logToDatabase) Methods.LogToDatabase(message, mType);
        }
    }
}
