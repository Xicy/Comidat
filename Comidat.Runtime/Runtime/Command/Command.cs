using System;

namespace Comidat.Runtime.Command
{
    /// <summary>
    ///     Command returns
    /// </summary>
    public enum CommandResult
    {
        /// <summary>
        ///     Command is successfull
        /// </summary>
        Okay,

        /// <summary>
        ///     command failed
        /// </summary>
        Fail,

        /// <summary>
        ///     command arguments invalid
        /// </summary>
        InvalidArgument,

        /// <summary>
        ///     stop the console reading
        /// </summary>
        Break
    }

    /// <summary>
    ///     Generalized command holder
    /// </summary>
    /// <typeparam name="TFunc">function of command</typeparam>
    public abstract class Command<TFunc> where TFunc : class
    {
        /// <summary>
        ///     constractor of command
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="usage">how to use command information</param>
        /// <param name="description">description of command</param>
        /// <param name="func">function of command</param>
        protected Command(string name, string usage, string description, TFunc func)
        {
            if (!typeof(TFunc).IsSubclassOf(typeof(Delegate)))
                throw new InvalidOperationException(string.Format(Localization.Get("Comidat.Util.Command.Command.Constructor.InvalidOperationException"), typeof(TFunc).Name));

            Name = name;
            Usage = usage;
            Description = description;
            Func = func;
        }

        public string Name { get; protected set; }
        public string Usage { get; protected set; }
        public string Description { get; protected set; }
        public TFunc Func { get; protected set; }
    }
}