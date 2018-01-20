using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Comidat.Diagnostics;

namespace Comidat.Windows.Client
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Logger.Archive = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");
            Logger.LogFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs", "Comidat.Client.log");

            ExceptionHandler.InstallExceptionHandler();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}