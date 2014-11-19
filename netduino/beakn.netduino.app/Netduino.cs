using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace beakn.netduino.app
{
    class Netduino
    {
        private OutputPort D10;
        private OutputPort D11;
        private OutputPort D12;

        public Netduino() { }

        public void Setup()
        {
            D10 = new OutputPort(Pins.GPIO_PIN_D10, false);
            D11 = new OutputPort(Pins.GPIO_PIN_D11, false);
            D12 = new OutputPort(Pins.GPIO_PIN_D12, false);
        }

        public void Set(string presence)
        {
            D10.Write(false);
            D11.Write(false);
            D12.Write(false);

            switch (presence)
            {
                case "DoNotDisturb":
                case "Busy":
                    D10.Write(true);
                    return;
                case "TemporarilyAway":
                case "Away":
                    D11.Write(true);
                    return;
                case "Free":
                    D12.Write(true);
                    return;
                default:
                    return;
            }
        }
    }
}
