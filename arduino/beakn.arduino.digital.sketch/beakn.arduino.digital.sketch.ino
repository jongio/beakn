#include <CmdMessenger.h>  // CmdMessenger

const int red = 5;
const int yellow = 6;
const int green = 7;

String msg = "";

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
  msg = cmdMessenger.readStringArg();
  
  if(msg == "Busy" || msg == "DoNotDisturb"){
    setColor(red);
  }else if(msg == "Away" || msg == "TemporarilyAway"){
    setColor(yellow);
  }else if (msg == "Free"){
    setColor(green);
  }else if (msg == "Offline"){
    reset(); 
  }
}

void reset(){
  digitalWrite(red, LOW);
  digitalWrite(yellow, LOW);
  digitalWrite(green, LOW);
}

void setColor(int color)
{
  reset();
  digitalWrite(color, HIGH);
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
  pinMode(red, OUTPUT);
  pinMode(yellow, OUTPUT);
  pinMode(green, OUTPUT);
}

// Loop function
void loop() 
{
  // Process incoming serial data, and perform callbacks
  cmdMessenger.feedinSerialData();
}


