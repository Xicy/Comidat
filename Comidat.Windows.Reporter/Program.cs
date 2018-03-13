using System;
using System.IO;
using System.Windows.Forms;
using Comidat.Diagnostics;

namespace Comidat
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Logger.Archive = Path.Combine(Environment.CurrentDirectory, "Logs");
            Logger.LogFile = Path.Combine(Environment.CurrentDirectory, "Logs", "Comidat.Reporter.log");

            ExceptionHandler.InstallExceptionHandler();

            //TODO:Unutma !!!!!!
            if ((DateTime.Parse("01/08/2018") - DateTime.Now).Days < 0)
                throw new ApplicationException("Application Crash Error Code:010818");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}