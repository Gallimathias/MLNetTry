using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris
{
    static class Program
    {
        private static Logger logger;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var config = new LoggingConfiguration();

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, new ColoredConsoleTarget("thea.logconsole"));
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, new FileTarget("thea.logfile") { FileName = "ml_tetris_thea.log" });

            LogManager.Configuration = config;
            logger = LogManager.GetCurrentClassLogger();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartUp());            
        }
    }
}
