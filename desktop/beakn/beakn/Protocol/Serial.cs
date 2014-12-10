using CommandMessenger;
using CommandMessenger.TransportLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public class Serial : Protocol
    {
        private SerialTransport serialTransport;
        private CmdMessenger cmdMessenger;

        public override void Setup()
        {
            // Create Serial Port object
            serialTransport = new SerialTransport();
            serialTransport.CurrentSerialSettings.PortName = Config.SerialCOMPort;    
            serialTransport.CurrentSerialSettings.BaudRate = Config.SerialBaudRate;   
            serialTransport.CurrentSerialSettings.DtrEnable = Config.SerialDtrEnable;     // For some boards (e.g. Sparkfun Pro Micro) DtrEnable may need to be true.

            // Initialize the command messenger with the Serial Port transport layer
            cmdMessenger = new CmdMessenger(serialTransport);

            // Tell CmdMessenger if it is communicating with a 16 or 32 bit Arduino board
            cmdMessenger.BoardType = Config.SerialBoardType;

            // Start listening
            cmdMessenger.StartListening();
        }

        public override void Send(string message)
        {
            try
            {
                var cmd = new SendCommand((int)SerialCommand.SetLed, message);
                var recieve = cmdMessenger.SendCommand(cmd);
                OnSendSuccess(new MessageEventArgs(string.Format("Message Send Success: Id={0}, Message={1}", recieve.RawString, message)));
            }
            catch (Exception e)
            {
                OnSendFailure(new MessageEventArgs("Message Send Failure: " + e.ToString()));
            }
        }

        enum SerialCommand
        {
            SetLed, // Command to request led to be set in specific state
        };
    }
}
