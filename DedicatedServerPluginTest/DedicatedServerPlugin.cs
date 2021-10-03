using NLog;
using Sandbox;
using Sandbox.ModAPI;
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

        MqttClientWrapper mqttClient;
        public void Init(object gameInstance)
        {
            mqttClient = new MqttClientWrapper(gameInstance as MySandboxGame);
        }

        public void Update()
        {
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
