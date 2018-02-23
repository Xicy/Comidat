#if !EF6
using System;
using System.Net;
using System.Threading.Tasks;
using Comidat.Net.EventArgs;
using MQTTnet;
using MQTTnet.Server;


namespace Comidat.Net
{
    /// <inheritdoc />
    /// <summary>
    ///     MQTT server with Server interface
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class MQTT : IServer
    {
        /// <summary>
        ///     variable of server acceses in class sending message and other things
        /// </summary>
        private readonly IMqttServer _server;

        /// <summary>
        ///     MQTT server constractor
        /// </summary>
        public MQTT()
        {
            //Create mqtt server
            _server = new MqttFactory().CreateMqttServer();
            //assign connected event args
            _server.ClientConnected += (sender, args) =>
                Connected?.Invoke(this, new ConnectedEventArgs(args.Client.ClientId));
            //assign disconnected event args
            _server.ClientDisconnected += (sender, args) =>
                Disconnected?.Invoke(this, new DisconnectedEventArgs(args.Client.ClientId));
            //assign message received event args
            _server.ApplicationMessageReceived += (sender, args) => MessageReceived?.Invoke(this,
                new MessageReceivedEventArgs(args.ClientId, args.ApplicationMessage.Payload));
        }

        /// <inheritdoc />
        public event EventHandler<ConnectedEventArgs> Connected;

        /// <inheritdoc />
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <inheritdoc />
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <inheritdoc />
        public Task StartAsync(IPEndPoint ipe)
        {
            //mqtt server options
            var options = new MqttServerOptions();
            //mqtt server port options
            options.DefaultEndpointOptions.Port = ipe.Port;
            //start mqtt server
            return _server.StartAsync(options);
        }

        /// <inheritdoc />
        public Task StopAsync()
        {
            //stop mqtt server
            return _server.StopAsync();
        }

        /// <summary>
        ///     Publish message from all client which is subs data topic
        /// </summary>
        /// <param name="arr">sending message of byte array</param>
        /// <returns></returns>
        public Task SendMessage(byte[] arr)
        {
            return _server.PublishAsync(new MqttApplicationMessageBuilder().WithTopic("data").WithPayload(arr)
                .WithAtLeastOnceQoS()
                .Build());
        }
    }
}
#endif