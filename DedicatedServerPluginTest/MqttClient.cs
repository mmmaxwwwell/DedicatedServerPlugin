
using NLog;
using Sandbox;
using Sandbox.ModAPI;
using System;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace DedicatedServerPluginTest
{
    class MqttClientWrapper
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

        MqttClient _client;
        MqttClient client
        {
            get
            {
                if (_client == null)
                {
                    _client = new MqttClient("329967105c1a42858a61da9523f94ee3.s1.eu.hivemq.cloud");
                }
                return _client;
            }
        }

        MySandboxGame gameInstance;
        Timer timer;

        public MqttClientWrapper(MySandboxGame gameInstance)
        {
            this.gameInstance = gameInstance;
            MyAPIGateway.Session.OnSessionReady += Session_OnSessionReady;
            timer = new Timer(timerCallback, null, 5000, 5000);
        }

        private void Session_OnSessionReady()
        {
            Console.Error.WriteLine("Session_OnSessionReady");
            connect();
        }

        static void timerCallback (object state)
        {
            Console.Error.WriteLine("Hello World!");
            Console.Error.WriteLine($"PlayerCount:{MyAPIGateway.Players.Count}");
        }

        public async Task connect()
        {
            log.Error("Connecting");
            Console.Error.WriteLine("Connecting");

            //try
            //{
            //    _client.Connect("clientId", "username", "password");
            //}
            //catch (Exception ex)
            //{
            //    Console.Error.WriteLine("Error connecting to server");
            //    log.Error(ex, "Error connecting to server");
            //}
        }
    }
}
