namespace Comidat.Net.EventArgs
{
    /// <summary>
    ///     Message Received event args for server
    /// </summary>
    public class MessageReceivedEventArgs : System.EventArgs
    {
        /// <summary>
        ///     Constractor
        /// </summary>
        /// <param name="client">Id of client</param>
        /// <param name="message">received message from client</param>
        public MessageReceivedEventArgs(string client, byte[] message)
        {
            Client = client;
            Message = message;
        }

        public string Client { get; }
        public byte[] Message { get; }
    }
}