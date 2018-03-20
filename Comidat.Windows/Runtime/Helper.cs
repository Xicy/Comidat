using System.Diagnostics;

namespace Comidat.Runtime
{
    public static partial class Helper
    {
        public static void AddFireWall()
        {
            const string ruleName = "Comidat Socket Server";//Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "advfirewall firewall delete rule name=\"" + ruleName + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden
                })?.WaitForExit();
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = "advfirewall firewall add rule name=\"" + ruleName +
                                "\" dir=in action=allow program=\"" + Process.GetCurrentProcess().MainModule.FileName +
                                "\" enable=yes",
                    WindowStyle = ProcessWindowStyle.Hidden
                })?.WaitForExit();
        }
    }
}
