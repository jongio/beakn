#include <CmdMessenger.h>  // CmdMessenger

const int redPin = 9;
const int bluePin = 10;
const int greenPin = 11;

String color = "";

// Attach a new CmdMessenger object to the default Serial port
CmdMessenger cmdMessenger = CmdMessenger(Serial);

enum
{
  kSetLed, // Command to request led to be set in specific state
};

// Callbacks define on which received commands we take action 
void attachCommandCallbacks()
{
  cmdMessenger.attach(kSetLed, OnSetLed);
}

// Callback function that sets led on or off
void OnSetLed()
{
  // Read led state argument, interpret string as boolean
  //ledState = cmdMessenger.readBoolArg();
  color = cmdMessenger.readStringArg();
  
  if(color == "Busy" || color == "DoNotDisturb"){
    setColor(255,0,0);
  }else if(color == "Away" || color == "TemporarilyAway"){
    setColor(20, 255, 0);
  }else if (color == "Free"){
    setColor(0, 255, 0);
  }else{
    setColor(0, 0, 0);
  }
}

void setColor(int red, int green, int blue)
{
  analogWrite(redPin, red);
  analogWrite(greenPin, green);
  analogWrite(bluePin, blue);
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

  // set pin for blink LED
  pinMode(redPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  pinMode(greenPin, OUTPUT);
}

// Loop function
void loop() 
{
  // Process incoming serial data, and perform callbacks
  cmdMessenger.feedinSerialData();
}


