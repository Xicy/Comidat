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
            //TODO:Unutma !!!!!!
            if ((DateTime.Parse("01/03/2018") - DateTime.Now).Days < 0)
                Environment.Exit(-1);

            Logger.Archive = Path.Combine(Environment.CurrentDirectory, "Logs");
            Logger.LogFile = Path.Combine(Environment.CurrentDirectory, "Logs", "Comidat.Client.log");

            ExceptionHandler.InstallExceptionHandler();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}