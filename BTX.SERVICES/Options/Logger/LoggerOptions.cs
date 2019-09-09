using System;
using System.Collections.Generic;
using System.Text;

namespace BTX.SERVICES.Options.Logger
{
    public class LoggerOptions
    {
        public bool logToFile { get; set; }
        public bool logToConsole { get; set; }
        public bool logMessage { get; set; }
        public bool logWarning { get; set; }
        public bool logError { get; set; }
        public bool logToDatabase { get; set; }
        public bool _initialized { get; set; }
    }
}
