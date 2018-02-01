using System;
using System.Collections.Generic;
using System.Linq;
using Comidat.Diagnostics;

namespace Comidat.Runtime.Command
{
    /// <summary>
    ///     Console command manager
    /// </summary>
    public class ConsoleCommands : CommandManager<ConsoleCommand, ConsoleCommandFunc>
    {
        /// <summary>
        ///     constractor of console command manager
        /// </summary>
        public ConsoleCommands()
        {
            //add simple commands
            //help
            Add(Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Help"),
                Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Usage.Help"),
                Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Description.Help"), HandleHelp);
            //cls -> clear console
            Add(Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.CLS"),
                Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Description.CLS"),
                HandleCleanScreen);
            //close program
            Add(Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Exit"),
                Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Description.Exit"),
                HandleExit);
            //show program status it should be overwritten
            Add(Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Status"),
                Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Description.Status"),
                HandleStatus);
            //#if DEBUG
            //open and close debug on console
            Add(Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Debug"), Localization.Get("Comidat.Util.Command.ConsoleCommands.Constructor.Description.Debug"),
                HandleDebug);
            //#endif
        }

        /// <summary>
        ///     Add command without usage info
        /// </summary>
        /// <param name="name">Name of command</param>
        /// <param name="description">Description of command</param>
        /// <param name="handler">Function of command</param>
        public void Add(string name, string description, ConsoleCommandFunc handler)
        {
            Add(new ConsoleCommand(name, "", description, handler));
        }

        /// <summary>
        ///     Add command
        /// </summary>
        /// <param name="name">Name of command</param>
        /// <param name="usage">usage info of command</param>
        /// <param name="description">Description of command</param>
        /// <param name="handler">Function of command</param>
        public void Add(string name, string usage, string description, ConsoleCommandFunc handler)
        {
            Add(new ConsoleCommand(name, usage, description, handler));
        }

        /// <summary>
        ///     Console reader for console
        /// </summary>
        public void Wait()
        {
            //Write info first
            Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.Wait.Help"));

            // read console until function breate it
            while (true)
            {
                //read line of console
                var line = Console.ReadLine();
                //line is null continue 
                if (string.IsNullOrEmpty(line)) continue;

                //parse arguments
                var args = ParseLine(line);
                //if arguments equal 0 continue
                if (args.Count == 0) continue;

                //get command from first arg
                //argName arg0 arg1 arg2 ....
                var command = GetCommand(args[0]);
                if (command == null)
                    Logger.Error(Localization.Get("Comidat.Util.Command.ConsoleCommands.Wait.Info.Unknown"), args[0]);

                //if command has function invoke it
                var result = command?.Func(line, args);
                //if result is break, break while loop
                if (result == CommandResult.Break) break;

                //if result fail, write command fail
                if (result == CommandResult.Fail)
                    Logger.Error(Localization.Get("Comidat.Util.Command.ConsoleCommands.Wait.Error.Fail"),
                        command.Name);
                //if command result invalid argument write argument usages
                else if (result == CommandResult.InvalidArgument)
                    Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.Wait.Info.Usage"), command.Name,
                        command.Usage);
            }
        }

        //#if DEBUG
        /// <summary>
        ///     simple command debug toggeler
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected CommandResult HandleDebug(string command, IList<string> args)
        {
            //change hide attirbute of logger
            Logger.Hide ^= LogLevel.Debug;
            Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleDebug.Info"), (Logger.Hide & LogLevel.Debug) == 0 ? Localization.Get("True") : Localization.Get("False"));
            return CommandResult.Okay;
        }
        //#endif

        /// <summary>
        ///     simple command clean scren
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected CommandResult HandleCleanScreen(string command, IList<string> args)
        {
            //console clear
            Console.Clear();
            //write header
            Cli.WriteHeader();
            return CommandResult.Okay;
        }

        /// <summary>
        ///     simple command write help screen
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected CommandResult HandleHelp(string command, IList<string> args)
        {
            //if args count equal 1 so only command name write helps command
            if (args.Count == 1)
            {
                //calculate maximum length of command name
                var maxLength = Commands.Values.Max(a => a.Name.Length);
                //write help info
                Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleHelp.Info.Available"));
                //write each command name and description
                foreach (var cmd in Commands.Values.OrderBy(a => a.Name))
                    Logger.Info("  {0,-" + (maxLength + 2) + "}{1}", cmd.Name, cmd.Description);
            }
            //else second arg is searching arg name
            else
            {
                //find arg in commands
                var consoleCommand = GetCommand(args[1]);
                //if null 
                if (consoleCommand == null)
                {
                    //write unknown command message
                    Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleHelp.Info.Unknown"),
                        args[1]);
                    //return fail type
                    return CommandResult.Fail;
                }

                //write command infos
                Logger.Info(Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleHelp.Info.Code"),
                    consoleCommand.Name,
                    string.IsNullOrWhiteSpace(consoleCommand.Usage)
                        ? Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleHelp.Info.Null")
                        : consoleCommand.Usage, consoleCommand.Description);
            }

            return CommandResult.Okay;
        }

        /// <summary>
        ///     simple command write status of program
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected CommandResult HandleStatus(string command, IList<string> args)
        {
            Logger.Status(Localization.Get("Comidat.Util.Command.ConsoleCommands.HandleStatus.Status"),
                Math.Round(GC.GetTotalMemory(false) / 1024f));
            return CommandResult.Okay;
        }

        /// <summary>
        ///     simple command close program
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected CommandResult HandleExit(string command, IList<string> args)
        {
            Cli.Exit(0, false);
            return CommandResult.Okay;
        }
    }

    /// <summary>
    ///     Console command holder
    /// </summary>
    public class ConsoleCommand : Command<ConsoleCommandFunc>
    {
        public ConsoleCommand(string name, string usage, string description, ConsoleCommandFunc func) : base(name,
            usage, description, func)
        {
        }
    }

    /// <summary>
    ///     console command function
    /// </summary>
    /// <param name="command">command line</param>
    /// <param name="args">Arguments for function</param>
    /// <returns></returns>
    public delegate CommandResult ConsoleCommandFunc(string command, IList<string> args);
}