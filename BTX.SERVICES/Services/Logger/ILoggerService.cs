using static BTX.SERVICES.Enums.Logger.LoggerEnums;

namespace BTX.SERVICES.Services.Logger
{
    public interface ILoggerService
    {
        void LogMessage(string message, MessageType mType);
    }
}
