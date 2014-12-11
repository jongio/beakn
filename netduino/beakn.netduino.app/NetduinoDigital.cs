using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using config = beakn.netduino.app.ConfigurationManager;

namespace beakn.netduino.app
{
    class NetduinoDigital : INetduino
    {
        private OutputPort redLed;
        private OutputPort yellowLed;
        private OutputPort greenLed;
        private bool invertedPins = false;

        public NetduinoDigital() { }

        public void Setup()
        {
            redLed = new OutputPort(Pins.GPIO_PIN_D10, false);
            yellowLed = new OutputPort(Pins.GPIO_PIN_D11, false);
            greenLed = new OutputPort(Pins.GPIO_PIN_D12, false);
            invertedPins = ConfigurationManager.InvertedPins;
        }

        public void Set(string presence)
        {
            redLed.Write(invertedPins);
            yellowLed.Write(invertedPins);
            greenLed.Write(invertedPins);

            switch (presence)
            {
                case "DoNotDisturb":
                case "Busy":
                    redLed.Write(!invertedPins);
                    return;
                case "TemporarilyAway":
                case "Away":
                    yellowLed.Write(!invertedPins);
                    return;
                case "Free":
                    greenLed.Write(!invertedPins);
                    return;
                default:
                    return;
            }
        }
    }
}
