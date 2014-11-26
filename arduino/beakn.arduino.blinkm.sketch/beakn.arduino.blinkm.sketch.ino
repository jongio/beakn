#include <CmdMessenger.h>  // CmdMessenger
#include <Wire.h>
#include <BlinkM_funcs.h>

#define blinkm_addr 0x00

byte r,g,b;

String color = "";

// Attach a new CmdMessenger object to the default Serial port
CmdMessenger cmdMessenger = CmdMessenger(Serial);

enum
{
  kSetLed, // Command used by C# code to communicate with Arduino via Serial
};

// Callbacks define on which received commands we take action 
void attachCommandCallbacks()
{
  cmdMessenger.attach(kSetLed, OnSetLed);
}

// Callback function that sets led on or off
void OnSetLed()
{
  color = cmdMessenger.readStringArg();
  
  if(color == "Busy" || color == "DoNotDisturb"){
    setColor(255,0,0);
  }else if(color == "Away" || color == "TemporarilyAway"){
    setColor(255, 128, 0);
  }else if (color == "Free"){
    setColor(0, 255, 0);
  }else{
    setColor(0, 0, 0);
  }
}

void setColor(byte red, byte green, byte blue)
{
      BlinkM_setRGB(blinkm_addr, red, green, blue);
}

// Setup function
void setup() 
{
  // Listen on serial connection for messages from the PC
  // 115200 is the max speed on Arduino Uno, Mega, with AT8u2 USB
  // Use 57600 for the Arduino Duemilanove and others with FTDI Serial
  Serial.begin(115200); 

  // Adds newline to every command
  cmdMessenger.printLfCr();   

  // Attach my application's user-defined callback methods
  attachCommandCallbacks();

  BlinkM_beginWithPower();
  BlinkM_stopScript(blinkm_addr); 
  setColor(0, 0, 0);
}

// Loop function
void loop() 
{
  // Process incoming serial data, and perform callbacks
  cmdMessenger.feedinSerialData();
}


