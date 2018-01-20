using System;
using System.Linq;
using System.Text;
using Comidat.Diagnostics;

namespace Comidat.Runtime
{
    public class Cli
    {
        //hold title for clean screen and write header
        private static string _title;

        //hold title for clean screen and write header
        private static string _header;

        //hold title for clean screen and write header
        private static ConsoleColor _color = ConsoleColor.DarkGray;

        /// <param name="title">Name of this server (for the console's title)</param>
        /// <param name="header">Header of this server</param>
        /// <param name="color">Color of the header</param>
        public static void WriteHeader(string title, string header, ConsoleColor color)
        {
            _color = color;
            _title = title;
            _header = header;
            WriteHeader();
        }

        /// <summary>
        ///     Write Header
        /// </summary>
        public static void WriteHeader()
        {
            if (_title != null) Console.Title = _title;

            Console.ForegroundColor = _color;
            var lines = _header.Split('\n');
            var max = lines.Max(l => l.Length);
            Console.WindowWidth = Math.Max(max + 1, Console.WindowWidth);
            var left = new StringBuilder().Append(' ', (Console.WindowWidth - max - 1) / 2).ToString();
            foreach (var line in lines)
                Console.WriteLine(left + line);

            Console.Write(new StringBuilder().Append('_', Console.WindowWidth).ToString());
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("");
        }

        /// <summary>
        ///     Prefixes window title with an asterisk.
        /// </summary>
        public static void LoadingTitle()
        {
            if (!Console.Title.StartsWith("* "))
                Console.Title = @"* " + Console.Title;
        }

        /// <summary>
        ///     Removes asterisks and spaces that were prepended to the window title.
        /// </summary>
        public static void RunningTitle()
        {
            Console.Title = Console.Title.TrimStart('*', ' ');
        }

        /// <summary>
        ///     Waits for the return key, and closes the application afterwards.
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="wait"></param>
        public static void Exit(int exitCode, bool wait = true)
        {
            if (wait)
            {
                Logger.Info(Localization.Get("Comidat.Util.Cli.Exit.PressEnter"));
                Console.ReadLine();
            }

            Logger.Info(Localization.Get("Comidat.Util.Cli.Exit.Exiting"));
            Environment.Exit(exitCode);
        }
    }
}