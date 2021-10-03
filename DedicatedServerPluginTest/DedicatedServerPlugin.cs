using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VRage.Plugins;

namespace DedicatedServerPluginTest
{
    public class DedicatedServerPlugin : IConfigurablePlugin
    {
        private PluginConfiguration m_configuration;

        MqttClient _mqttClient;
        MqttClient mqttClient { 
            get { 
                if(_mqttClient == null) _mqttClient = new MqttClient();
                return _mqttClient;
            } 
        }

        public void Init(object gameInstance)
        {
            Console.Error.WriteLine("about to load mqtt");
            var loadit = mqttClient;
            Console.Error.WriteLine("post load mqtt");
            Console.Error.WriteLine(mqttClient);
        }

        public void Update()
        {
            Console.Write('.');
        }

        public IPluginConfiguration GetConfiguration(string userDataPath)
        {
            if (m_configuration == null)
            {
                string configFile = Path.Combine(userDataPath, "DedicatedServerPlugin.cfg");
                if (File.Exists(configFile))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PluginConfiguration));
                    using (FileStream stream = File.OpenRead(configFile))
                    {
                        m_configuration = serializer.Deserialize(stream) as PluginConfiguration;
                    }
                }

                if (m_configuration == null)
                {
                    m_configuration = new PluginConfiguration();
                }
            }

            return m_configuration;
        }

        public void Dispose()
        {
        }

        public string GetPluginTitle()
        {
            return "DS Test Plugin";
        }
    }
}
