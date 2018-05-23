using System.IO;
using NLog;

namespace RetroClash
{
    public class Logger
    {
        private static NLog.Logger _logger;

        public Logger()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            _logger = LogManager.GetCurrentClassLogger();
        }
    }
}