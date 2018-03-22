using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Comidat.Diagnostics
{
    public static class ExceptionHandler
    {
        public static void InstallExceptionHandler()
        {
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                e.SetObserved();
                Logger.Exception(e.Exception);
            };

            if (Debugger.IsAttached) return;

            AppDomain.CurrentDomain.FirstChanceException += (s, e) =>
                Logger.Exception(e.Exception);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is EntryPointNotFoundException) return;
                var ee = (Exception)e.ExceptionObject;
                Logger.Exception(ee);
            };

        }
    }
}