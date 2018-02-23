using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Comidat.Diagnostics;
using Comidat.Net.EventArgs;
using Comidat.Runtime;

namespace Comidat.Net
{
    /// <summary>
    ///     TCP server inherith from IServer
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TCP : IServer
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
        private TcpListener _server;

        /// <summary>
        ///     Constractor of TCP server
        /// </summary>
        public TCP()
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
        public
#if !EF6
            async
#endif
            Task StartAsync(IPEndPoint ipe)
        {
            // create server and put ip and port
            _server = new TcpListener(ipe);
            // init id from 0
            _id = 0;
            // start server
            _server.Start();
            // register stop method for cancellation token
            _cancellationToken.Register(_server.Stop);
            // while loop until server stopped
            while (!_cancellationToken.IsCancellationRequested)
                //safe for exceptions 
                try
                {
                    // accept client connection
#if EF6
                    var client = _server.AcceptSocket();
#else
                    var client = await _server.AcceptSocketAsync().ConfigureAwait(false);
#endif
                    // create state object for receive data from tcp connection
                    var so = new StateObject(_id++.ToString(), client);
                    // if connected not null Invoke connected event args
                    Connected?.Invoke(this, new ConnectedEventArgs(so.Id));
                    // and start receive message 
                    client.BeginReceive(so.Buffer, 0, StateObject.BufferSize, 0, Receive, so);
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

#if EF6
            return Task.Factory.StartNew(() => 0);
#endif
        }

        /// <inheritdoc />
        public Task StopAsync()
        {
            //stop server
            _server.Stop();
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

        /// <summary>
        ///     Async receiver for tcp client
        /// </summary>
        /// <param name="ar">State object carrier </param>
        private void Receive(IAsyncResult ar)
        {
            //Check server is stopped ?
            if (_cancellationToken.IsCancellationRequested)
                return;
            //get state object
            var state = (StateObject) ar.AsyncState;
            //socket for handle message
            var handler = state.Socket;
            // get received bytes 
            var bytesRead = handler.EndReceive(ar);

            //byte if bigger than 0 client is alive not server clossed 
            if (bytesRead > 0)
            {
                //clone buffer object for resize the event args
                var dt = state.Buffer;
                //resize array for sending event args
                Array.Resize(ref dt, bytesRead);
                //trigger message received event arg with message
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs(state.Id, dt));
                //continue reveceive message 
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, Receive, state);
            }
            else
            {
                //trigger disconnected client 
                Disconnected?.Invoke(this, new DisconnectedEventArgs(state.Id));
            }
        }

        /// <summary>
        ///     State object for tcp client receive message
        /// </summary>
        protected class StateObject
        {
            /// <summary>
            ///     Buffer size of one packet
            /// </summary>
            internal const int BufferSize = 1024 * 8; //8Kb

            /// <summary>
            ///     buffer for received message
            /// </summary>
            public byte[] Buffer = new byte[BufferSize];

            /// <summary>
            ///     virtual id of client
            /// </summary>
            public string Id;

            /// <summary>
            ///     socket for dont lose connection
            /// </summary>
            public Socket Socket;

            /// <summary>
            ///     constractor of class
            /// </summary>
            /// <param name="id">Virtual Id of client</param>
            /// <param name="s">Socket of client</param>
            public StateObject(string id, Socket s)
            {
                Id = id;
                Socket = s;
            }
        }
    }
}