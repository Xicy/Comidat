using System;
using System.Diagnostics;
using UnityEngine;


namespace Comidat.Diagnostics
{
    public static class ExceptionHandler
    {
        public static void InstallExceptionHandler()
        {
            /*TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                e.SetObserved();
                Logger.Exception(e.Exception);
            };*/

            if (Debugger.IsAttached) return;
            /*
             AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                Logger.Exception(e.Exception);
            */

            Application.logMessageReceived += (condition, trace, type) =>
                {
                    if (type == LogType.Exception)
                        Logger.Exception(new Exception(condition), trace);
                };
            Application.logMessageReceivedThreaded += (condition, trace, type) =>
            {
                if (type == LogType.Exception)
                    Logger.Exception(new Exception(condition), trace);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ee = (Exception)e.ExceptionObject;
                Logger.Exception(ee);
            };

        }
    }
}