namespace Comidat.Net.EventArgs
{
    /// <summary>
    ///     Disconnect event args for server
    /// </summary>
    public class DisconnectedEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Constractor
        /// </summary>
        /// <param name="client">Id of client</param>
        public DisconnectedEventArgs(string client)
        {
            Client = client;
        }

        public string Client { get; }
    }
}