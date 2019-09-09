using System;
using Microsoft.AspNetCore.Mvc;
using BTX.SERVICES.Services.Logger;
using static BTX.SERVICES.Enums.Logger.LoggerEnums;

namespace BTX.CORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerService _loggerService;

        public HomeController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public IActionResult Index()
        {
            _loggerService.LogMessage("something", MessageType.Error);

            try
            {
                //~~~~ do something
            }
            catch (Exception ex)
            {
                _loggerService.LogMessage(ex.Message, MessageType.Error);
                throw;
            }
            return View();
        }
    }
}