using System;
using System.Threading;
using CommandMessenger;
using CommandMessenger.TransportLayer;
using Microsoft.Lync.Model;

namespace beakn.arduino.console
{
    enum Command
    {
        SetLed, // Command to request led to be set in specific state
    };

    public class ArduinoSerial
    {
        public bool RunLoop { get; set; }
        private SerialTransport _serialTransport;
        private CmdMessenger _cmdMessenger;

        // Setup function
        public void Setup()
        {
            // Create Serial Port object
            _serialTransport = new SerialTransport();
            _serialTransport.CurrentSerialSettings.PortName = "COM5";    // Set com port
            _serialTransport.CurrentSerialSettings.BaudRate = 115200;     // Set baud rate
            _serialTransport.CurrentSerialSettings.DtrEnable = false;     // For some boards (e.g. Sparkfun Pro Micro) DtrEnable may need to be true.
            
            // Initialize the command messenger with the Serial Port transport layer
            _cmdMessenger = new CmdMessenger(_serialTransport);

            // Tell CmdMessenger if it is communicating with a 16 or 32 bit Arduino board
            _cmdMessenger.BoardType = BoardType.Bit16;
            
            // Attach the callbacks to the Command Messenger
            AttachCommandCallBacks();
            
            // Start listening
            _cmdMessenger.StartListening();                                
        }

        // Loop function
        public void Loop()
        {
        }

        public void Set(ContactAvailability avail)
        {
            _cmdMessenger.SendCommand(new SendCommand((int)Command.SetLed, avail.ToString()));
        }

        // Exit function
        public void Exit()
        {
            // We will never exit the application
        }

        /// Attach command call backs. 
        private void AttachCommandCallBacks()
        {
            // No callbacks are currently needed
        }
    }
}
