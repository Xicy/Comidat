using System;
using System.Net;
using System.Threading.Tasks;
using Comidat.Net.EventArgs;

namespace Comidat.Net
{
    /// <summary>
    ///     Interface of server
    /// </summary>
    public interface IServer
    {
        /// <summary>
        ///     Start server async
        /// </summary>
        /// <param name="ipe">address of server which runs in port and ip</param>
        /// <returns></returns>
        Task StartAsync(IPEndPoint ipe);

        /// <summary>
        ///     Stop server async
        /// </summary>
        /// <returns></returns>
        Task StopAsync();

        /// <summary>
        ///     send message to client
        /// </summary>
        /// <param name="arr">message type of byte array</param>
        /// <returns></returns>
        Task SendMessage(byte[] arr);

        /// <summary>
        ///     Connected Eventargs
        /// </summary>
        event EventHandler<ConnectedEventArgs> Connected;

        /// <summary>
        ///     Disconnected Eventargs
        /// </summary>
        event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        ///     MessageReceived Eventargs
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}