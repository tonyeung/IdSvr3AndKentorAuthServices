using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogging();

            using (WebApp.Start<Startup>("http://localhost:13856/"))
            {
                Console.WriteLine("server running...");
                Console.ReadLine();
            }
        }

        public static void ConfigureLogging()
        {
            var layout = new PatternLayout
            {
                ConversionPattern = "%d [%t] %-5p %c [%x] - %m%n"
            };
            layout.ActivateOptions();
            var consoleAppender = new ColoredConsoleAppender
            {
                Threshold = Level.Debug,
                Layout = layout
            };
            consoleAppender.ActivateOptions();
            var fileAppender = new RollingFileAppender
            {
                DatePattern = "yyyy-MM-dd'.txt'",
                MaxFileSize = 10 * 1024 * 1024,
                MaxSizeRollBackups = 10,
                StaticLogFileName = false,
                File = @"d:\logs\auth",
                Layout = layout,
                AppendToFile = true,
                Threshold = Level.Debug,
            };

#if DEBUG
            fileAppender.File = @"log_";
#endif

            fileAppender.ActivateOptions();

            BasicConfigurator.Configure(fileAppender, consoleAppender);
        }
    }
}
