using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Comidat.Diagnostics;
using Comidat.Net.EventArgs;
using Comidat.Runtime;
#pragma warning disable 414
#pragma warning disable 67
namespace Comidat.Net
{
    /// <summary>
    ///     TCP server inherith from IServer
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class UDP : IServer
    {
        /// <summary>
        ///     check async control when server stop triggered
        /// </summary>
        private CancellationToken _cancellationToken;

        /// <summary>
        ///     virtual client id genreted from integer and each client increase 1
        /// </summary>
        private int _id;

        /// <summary>
        ///     server usign reach in class
        /// </summary>
        private UdpClient _server;

        /// <summary>
        ///     Constractor of TCP server
        /// </summary>
        public UDP()
        {
            // create cancellation token for signal
            _cancellationToken = new CancellationToken(false);
        }

        /// <inheritdoc />
        public event EventHandler<ConnectedEventArgs> Connected;

        /// <inheritdoc />
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <inheritdoc />
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <inheritdoc />
        /// <param name="ipe">IP and Port for server to start</param>
        /// <returns></returns>
        public async Task StartAsync(IPEndPoint ipe)
        {
            // create server and put ip and port
            _server = new UdpClient(ipe);
            // init id from 0
            _id = 0;

            // register stop method for cancellation token
            _cancellationToken.Register(_server.Close);
            // while loop until server stopped
            while (!_cancellationToken.IsCancellationRequested)
                //safe for exceptions 
                try
                {
                    // accept client connection
                    var client = await _server.ReceiveAsync();
                    MessageReceived?.Invoke(this,
                        new MessageReceivedEventArgs(client.RemoteEndPoint.Address.ToString(), client.Buffer));
                }
                //if objecet is disposed and server stopeed give info
                catch (ObjectDisposedException) when (_cancellationToken.IsCancellationRequested)
                {
                    Logger.Info(Localization.Get("Comidat.Controller.Server.TCP.StartAsync.ObjectDisposedException"));
                }
                //other exception write in log
                catch (Exception ex)
                {
                    Logger.Exception(ex, Localization.Get("Comidat.Controller.Server.TCP.StartAsync.Exception"),
                        ex.Message);
                }
        }

        /// <inheritdoc />
        public Task StopAsync()
        {
            //stop server
            _server.Close();
            // for async task 
            return null;
        }

        /// <summary>
        ///     This method not working in TCP client
        /// </summary>
        [Obsolete("This method not working in TCP client", true)]
        public Task SendMessage(byte[] arr)
        {
            throw new NotImplementedException();
        }
    }
}