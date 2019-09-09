using BTX.SERVICES.Options.Logger;
using BTX.SERVICES.Services.Logger;
using BTX.XTEST.Helpers.Shared;
using Microsoft.Extensions.Options;
using System;
using Xunit;
using static BTX.SERVICES.Enums.Logger.LoggerEnums;

namespace BTX.XTEST.Test.Logger
{
    public class JobLoggerTest
    {
        private LoggerService _loggerService;
        private LoggerOptions _loggerOptions { get; set; }

        [Fact]
        public void LoggerTest()
        {
            _loggerOptions = BindHelpers.FillLoggerOptions();
            var options = Options.Create(_loggerOptions);
            _loggerService = new LoggerService(options);

            Action action = () => _loggerService.LogMessage("hi..", MessageType.Error);

            AssertHelpers.DoesNotThrows<Exception>(action);
        }


    }
}
