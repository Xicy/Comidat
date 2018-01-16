using System.Collections.Generic;

namespace Comidat.Runtime.Command
{
    /// <summary>
    ///     Generalized command manager
    /// </summary>
    /// <typeparam name="TCommand">command informations</typeparam>
    /// <typeparam name="TFunc">function of command</typeparam>
    public abstract class CommandManager<TCommand, TFunc> where TCommand : Command<TFunc> where TFunc : class
    {
        /// <summary>
        ///     collect all command in this list
        /// </summary>
        protected Dictionary<string, TCommand> Commands;

        /// <summary>
        ///     protected command for not inherit from class
        /// </summary>
        protected CommandManager()
        {
            //init list
            Commands = new Dictionary<string, TCommand>();
        }

        /// <summary>
        ///     Adds command to list of command handlers.
        /// </summary>
        /// <param name="command">Command informations</param>
        protected void Add(TCommand command)
        {
            //add in list new command
            Commands[command.Name] = command;
        }

        /// <summary>
        ///     Returns arguments parsed from line.
        /// </summary>
        /// <remarks>
        ///     Matches words and multiple words in quotation.
        /// </remarks>
        /// <example>
        ///     arg0 arg1 arg2 -- 3 args: "arg0", "arg1", and "arg2"
        ///     arg0 arg1 "arg2 arg3" -- 3 args: "arg0", "arg1", and "arg2 arg3"
        /// </example>
        protected IList<string> ParseLine(string line)
        {
            //create args list
            var args = new List<string>();
            // check if next char is '"' (quote)
            var quote = false;
            //check each character
            for (int i = 0, n = 0; i <= line.Length; ++i)
            {
                //(end line or line char is whitespace) or not quoted args 
                if ((i == line.Length || line[i] == ' ') && !quote)
                {
                    if (i - n > 0)
                        //add arguments
                        args.Add(line.Substring(n, i - n).Trim(' ', '"'));

                    n = i + 1;
                    continue;
                }

                //chech if quote character in char
                if (line[i] == '"')
                    quote = !quote;
            }

            //return arguments for function
            return args;
        }

        /// <summary>
        ///     Returns command or null, if the command doesn't exist.
        /// </summary>
        /// <param name="name">command name</param>
        /// <returns></returns>
        public TCommand GetCommand(string name)
        {
            //try get command if been in list
            Commands.TryGetValue(name, out var command);
            return command;
        }
    }
}