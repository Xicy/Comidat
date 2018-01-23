using System;
using System.Globalization;
using System.IO;
using Comidat.Runtime;

namespace Comidat.Diagnostics
{
    /// <summary>
    ///     Log level flags
    /// </summary>
    [Flags]
    public enum LogLevel : byte
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 4,
        Debug = 8,
        Status = 16,
        Exception = 32,
        Unimplemented = 64
    }

    /// <summary>
    ///     Logs messages to command line and file.
    /// </summary>
    public static class Logger
    {
        private static readonly object SyncRoot = new object();
        private static string _logFile;

        /// <summary>
        ///     Specifies the log levels that shouldn't be displayed
        ///     on the command line.
        /// </summary>
        public static LogLevel Hide { get; set; }

        /// <summary>
        ///     Sets or returns the directory in which the logs are archived.
        ///     If no archive is set, log files will simply be overwritten.
        /// </summary>
        public static string Archive { get; set; }

        /// <summary>
        ///     Sets or returns the file to log to. Upon setting, the file will
        ///     be deleted. If Archive is set, it will be moved to safety first.
        /// </summary>
        public static string LogFile
        {
            get => _logFile;
            set
            {
                if (value != null)
                {
                    var pathToFile = Path.GetDirectoryName(value);

                    if (!Directory.Exists(pathToFile))
                        Directory.CreateDirectory(pathToFile ?? throw new ArgumentNullException());

                    if (File.Exists(value))
                    {
                        if (Archive != null)
                        {
                            if (!Directory.Exists(Archive))
                                Directory.CreateDirectory(Archive);

                            var time = File.GetLastWriteTime(value);
                            var archive = Path.Combine(Archive, time.ToString("yyyy-MM-dd_HH-mm"));
                            var archiveFilePath = Path.Combine(archive, Path.GetFileName(value));

                            if (!Directory.Exists(archive))
                                Directory.CreateDirectory(archive);

                            if (File.Exists(archiveFilePath))
                                File.Delete(archiveFilePath);

                            File.Move(value, archiveFilePath);
                        }

                        File.Delete(value);
                    }
                }

                _logFile = value;
            }
        }

        #region Writers

        public static void Info(string format, params object[] args)
        {
            WriteLine(LogLevel.Info, format, args);
        }

        public static void Warning(string format, params object[] args)
        {
            WriteLine(LogLevel.Warning, format, args);
        }

        public static void Error(string format, params object[] args)
        {
            WriteLine(LogLevel.Error, format, args);
        }

        public static void Debug(string format, params object[] args)
        {
            WriteLine(LogLevel.Debug, format, args);
        }

        public static void Debug(object obj)
        {
            WriteLine(LogLevel.Debug, obj.ToString());
        }

        public static void Status(string format, params object[] args)
        {
            WriteLine(LogLevel.Status, format, args);
        }

        public static void Exception(Exception ex, string description = null, params object[] args)
        {
            if (description != null)
            {
                if (Helper.HasFlag((byte) Hide, (byte) LogLevel.Exception))
                    description += " " + Localization.Get("Comidat.Util.Logger.Exception.Description");

                WriteLine(LogLevel.Error, description, args);
            }

            WriteLine(LogLevel.Exception, ex.ToString());
        }

        public static void Unimplemented(string format, params object[] args)
        {
            WriteLine(LogLevel.Unimplemented, format, args);
        }

        public static void Progress(int current, int max)
        {
            //TODO:Linux Control Close for linux
            var maxW = Console.WindowWidth - 50;//Console.WindowWidth - 50;
            var donePerc = 100f / max * current;
            var done = (int) Math.Ceiling((float) maxW / max * current);

            Write(LogLevel.Info, false, "[" + "".PadRight(done, '#') + "".PadLeft(maxW - done, '.') + "] {0,5}%\r",
                donePerc.ToString("0.0", CultureInfo.InvariantCulture));
            if (Math.Abs(donePerc - 100F) < 0.1) Write(LogLevel.Info, false, "\n");
        }

        public static void WriteLine(LogLevel level, string format, params object[] args)
        {
            Write(level, format + Environment.NewLine, args);
        }

        public static void WriteLine()
        {
            WriteLine(LogLevel.None, "");
        }

        public static void Write(LogLevel level, string format, params object[] args)
        {
            Write(level, true, format, args);
        }

        private static string GetLocalization(LogLevel level)
        {
            var r = "Comidat.Util.LogLevel.";
            switch (level)
            {
                case LogLevel.Info:
                    r += "Info";
                    break;
                case LogLevel.Warning:
                    r += "Warning";
                    break;
                case LogLevel.Error:
                    r += "Error";
                    break;
                case LogLevel.Debug:
                    r += "Debug";
                    break;
                case LogLevel.Status:
                    r += "Status";
                    break;
                case LogLevel.Exception:
                    r += "Exception";
                    break;
                case LogLevel.Unimplemented:
                    r += "Unimplemented";
                    break;
            }

            return r;
        }

        private static void Write(LogLevel level, bool toFile, string format, params object[] args)
        {
            lock (SyncRoot)
            {
                if (!Helper.HasFlag((byte) Hide, (byte) level))
                {
                    switch (level)
                    {
                        case LogLevel.Info:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case LogLevel.Warning:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case LogLevel.Error:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case LogLevel.Debug:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case LogLevel.Status:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case LogLevel.Exception:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case LogLevel.Unimplemented:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                    }

                    if (level != LogLevel.None)
                        Console.Write(@"[{0}]", Localization.Get(GetLocalization(level)));

                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (level != LogLevel.None)
                        Console.Write(@" - ");

                    Console.Write(format, args);
                }

                if (_logFile != null && toFile)
                    using (var file = new StreamWriter(_logFile, true))
                    {
                        file.Write(DateTime.Now + " ");
                        if (level != LogLevel.None)
                            file.Write("[{0}] - ", Localization.Get(GetLocalization(level)));
                        file.Write(format, args);
                        file.Flush();
                    }
            }
        }

        #endregion
    }
}