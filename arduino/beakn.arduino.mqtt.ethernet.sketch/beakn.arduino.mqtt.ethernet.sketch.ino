#include <SPI.h>
#include <Ethernet.h>
#include "PubSubClient.h"

void callback(char* topic, byte* payload, unsigned int length); // This is the signature for the mqtt callback method

// Update these with values suitable for your network.
byte mac[] = { 0xDE, 0xED, 0xBA, 0xFE, 0xFE, 0xED }; // I kept the same as the examples and didn't run into any issues.
byte mqttServer[] = { 212, 72, 74, 21 }; // This is the IP of the mqtt broker

char topicName[] = "/beakn/1"; //The "1" is the pairingCode - You'll need to change this to match the pairingCode you use on the client.
char clientName[] = "beakn-arduino-client-mqtt-ethernet-1"; // The "1" uniquely identifies this client. Change it to something you unique per client.
char payloadBuffer[20]; // Buffer to hold payload of the message as soon as it comes into the callback

const int red = 5;
const int yellow = 6;
const int green = 7;


EthernetClient ethClient;
PubSubClient client(mqttServer, 1883, callback, ethClient);

void callback(char* topic, byte* payload, unsigned int length) { // This gets called every time a message is received
  Serial.println("Topic: " + String(topic));
  Serial.println(length, DEC);
  
  // create character buffer with ending null terminator (string)
  int i = 0; // Declare this outside of loop so we can null term.

  for(i=0; i<length; i++) {
    payloadBuffer[i] = payload[i];
  }
  payloadBuffer[i] = '\0';
  
  Serial.println(payloadBuffer);
  
  setColor(String(payloadBuffer));
}

void setup()
{
  Serial.begin(9600);
  
  pinMode(red, OUTPUT);
  pinMode(yellow, OUTPUT);
  pinMode(green, OUTPUT);
  
  if(Ethernet.begin(mac) == 0){
    Serial.println("Failed to configure Ethernet using DHCP");
  }else{
   delay(1000); // Give ethernet shield a second to init
   Serial.println("Connecting...");
  
    if (!client.connected()) {
    
      Serial.print("Connecting to MQTT server");
      
      if (client.connect(clientName)) {
        Serial.println("Connected to MQTT server");
        
        Serial.print("Attempting to Subscribe to topic: ");
        Serial.println(topicName);
        
        if(client.subscribe(topicName)){
          Serial.println("Successfully subscribed to topic");
        }else{
          Serial.println("Failed to subscribe to topic");
        }
      }else {
        Serial.println("Unable to connect to MQTT server");
      }
    }
  }
}

void loop()
{
  client.loop();
}

// Callback function that sets led on or off
void setColor(String msg)
{
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

void setColor(int color)
{
  reset();
  digitalWrite(color, HIGH);
}

void reset(){
  digitalWrite(red, LOW);
  digitalWrite(yellow, LOW);
  digitalWrite(green, LOW);
}




