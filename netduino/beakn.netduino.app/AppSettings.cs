using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Xml;

namespace beakn.netduino.app
{
    public  class AppSettings
    {
        private const string APPSETTINGS_SECTION = "appSettings";
        private const string ADD = "add";
        private const string KEY = "key";
        private const string VALUE = "value";

        private Hashtable store;


        public AppSettings()
        {
            store = new Hashtable();
            Load();
        }

        public string this[string key]
        {
            get
            {
                return GetAppSetting(key);
            }
        }

        public string GetAppSetting(string key)
        {
            return GetAppSetting(key, null);
        }

        public string GetAppSetting(string key, string defaultValue)
        {
            if (!store.Contains(key))
                return defaultValue;
            return (string)store[key];
        }

        public void Load()
        {
            try
            {
                //TODO - Research why this wasn't working - likely because Netduino doesn't support the card (SDHC) or it's too big. I think it only supports up to 2GB
                // A first chance exception of type 'System.NotSupportedException' occurred in Microsoft.SPOT.IO.dll
                // A first chance exception of type 'System.IO.IOException' occurred in System.IO.dll
                //FileStream fs = new FileStream(@"\SD\App.config", FileMode.Open, FileAccess.Read);
                //Load(fs);

                store["MqttHost"] = "212.72.74.21"; //broker.mqttdashboard.com
                store["MqttPort"] = "1883";
                store["MqttUsername"] = "";
                store["MqttPassword"] = "";
                store["MqttPairingCode"] = "1";
                store["MqttTopicRoot"] = "/beakn/";
                store["MqttTopic"] = store["MqttTopicRoot"].ToString() + store["MqttPairingCode"].ToString();
                store["MqttClientName"] = "beakn-netduino-client";
                store["MqttClientId"] = store["MqttClientName"] + Guid.NewGuid().ToString();
                store["LedPinType"] = "Digital";
                store["InvertedPins"] = "true";
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }

        }

        public void Load(Stream xmlStream)
        {
            using (XmlReader reader = XmlReader.Create(xmlStream))
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case APPSETTINGS_SECTION:
                            while (reader.Read())
                            {
                                if (reader.Name == APPSETTINGS_SECTION)
                                    break;

                                if (reader.Name == ADD)
                                {
                                    var key = reader.GetAttribute(KEY);
                                    var value = reader.GetAttribute(VALUE);

                                    //Debug.Print(key + "=" + value);
                                    store.Add(key, value);
                                }
                            }

                            break;
                    }
                }
            }
        }

    }
}
