using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace beakn.netduino.app
{
    class NetduinoAnalog : INetduino
    {
        private SecretLabs.NETMF.Hardware.PWM redLed;
        private SecretLabs.NETMF.Hardware.PWM blueLed;
        private SecretLabs.NETMF.Hardware.PWM greenLed;

        public NetduinoAnalog() { }

        public void Setup()
        {
            redLed = new SecretLabs.NETMF.Hardware.PWM(Pins.GPIO_PIN_D9);
            blueLed = new SecretLabs.NETMF.Hardware.PWM(Pins.GPIO_PIN_D10);
            greenLed = new SecretLabs.NETMF.Hardware.PWM(Pins.GPIO_PIN_D11);
        }

        public void Set(string presence)
        {
            redLed.SetDutyCycle(0);
            blueLed.SetDutyCycle(0);
            greenLed.SetDutyCycle(0);

            switch (presence)
            {
                case "DoNotDisturb":
                case "Busy":
                    redLed.SetDutyCycle(100);
                    return;
                case "TemporarilyAway":
                case "Away":
                    redLed.SetDutyCycle(30);
                    greenLed.SetDutyCycle(100);
                    return;
                case "Free":
                    greenLed.SetDutyCycle(100);
                    return;
                default:
                    return;
            }
        }
    }
}
