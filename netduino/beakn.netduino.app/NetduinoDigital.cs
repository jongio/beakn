using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace beakn.netduino.app
{
    class NetduinoDigital : INetduino
    {
        private OutputPort redLed;
        private OutputPort yellowLed;
        private OutputPort greenLed;

        public NetduinoDigital() { }

        public void Setup()
        {
            redLed = new OutputPort(Pins.GPIO_PIN_D9, false);
            yellowLed = new OutputPort(Pins.GPIO_PIN_D10, false);
            greenLed = new OutputPort(Pins.GPIO_PIN_D11, false);
        }

        public void Set(string presence)
        {
            redLed.Write(false);
            yellowLed.Write(false);
            greenLed.Write(false);

            switch (presence)
            {
                case "DoNotDisturb":
                case "Busy":
                    redLed.Write(true);
                    return;
                case "TemporarilyAway":
                case "Away":
                    yellowLed.Write(true);
                    return;
                case "Free":
                    greenLed.Write(true);
                    return;
                default:
                    return;
            }
        }
    }
}
