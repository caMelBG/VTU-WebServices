using NLog;
using NLog.Config;
using NLog.Targets;

namespace SOAP
{
    public class NLogConfig
    {
        public static void Configurate()
        {
            var config = new LoggingConfiguration();
            var logfile = new FileTarget("logfile") { FileName = "file.txt" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;
            LogManager.GetCurrentClassLogger();
        }
    }
}