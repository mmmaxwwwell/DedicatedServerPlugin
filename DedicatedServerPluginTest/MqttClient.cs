using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DedicatedServerPluginTest
{
    class MqttClient
    {
        Logger _log;
        Logger log
        {
            get
            {
                if (_log == null) _log = LogManager.GetCurrentClassLogger();
                return _log;
            }
        }

        IMqttClient _client;
        IMqttClient client
        {
            get
            {
                if (_client == null)
                {
                    var factory = new MqttFactory();
                    _client = factory.CreateMqttClient();

                    _client.UseApplicationMessageReceivedHandler((messageEventArgs) =>
                    {

                    });

                    _client.UseConnectedHandler((connectedEventArgs) =>
                    {
                        log.Info("Connected!");
                    });

                    _client.UseDisconnectedHandler((disconnectedEventArgs) =>
                    {
                        log.Info("Disconnected");
#pragma warning disable 4014
                        connect(5);
#pragma warning restore 4014
                    });
                }
                return _client;
            }
        }

        public MqttClient()
        {
            //log.Info("Initializing DedicatedServerPluginTest.MqttClient");
#pragma warning disable 4014
            //connect();
#pragma warning restore 4014
        }

        public async Task connect(int delaySeconds = 0)
        {

            if (delaySeconds != 0) await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            var options = new MqttClientOptionsBuilder()
                .WithClientId("clientId")
                .WithTcpServer("hostname")
                .WithCredentials("username", "password")
                .WithTls()
                .WithCleanSession()
                .Build();

            try
            {
                await client.ConnectAsync(options, CancellationToken.None);

            }
            catch (Exception ex)
            {
                log.Error(ex, "Error connecting to server");
            }
        }
    }
}
