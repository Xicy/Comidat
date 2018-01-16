namespace Comidat.Net.EventArgs
{
    /// <summary>
    ///     Server connected event args
    /// </summary>
    public class ConnectedEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Constractor
        /// </summary>
        /// <param name="client">Id of client</param>
        public ConnectedEventArgs(string client)
        {
            Client = client;
        }

        public string Client { get; }
    }
}