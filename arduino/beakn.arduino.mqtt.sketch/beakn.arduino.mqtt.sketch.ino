/*
Example code showing an Arduino Uno with a Wifi Shield and BlinkM subscribing to MQTT messages sent from desktop.
*/

#include <SPI.h>
#include <WiFi.h>
#include "PubSubClient.h"
#include <Wire.h>
#include <BlinkM_funcs.h>

void callback(char* topic, byte* payload, unsigned int length); // This is the signature for the mqtt callback method

// Update these with values suitable for your network.
byte mac[] = { 0xDE, 0xED, 0xBA, 0xFE, 0xFE, 0xED }; // I kept the same as the examples and didn't run into any issues.
byte server[] = { 212, 72, 74, 21 }; // This is the IP of the mqtt broker
char ssid[] = "ssid"; // your network SSID (name)
char pass[] = "pass"; // your network password
char topicName[] = "/beakn/1"; //The "1" is the pairingCode - You'll need to change this to match the pairingCode you use on the client.
char clientName[] = "beakn-arduino-client-1"; // The "1" uniquely identifies this client. Change it to something you unique per client.
char payloadBuffer[20]; // Buffer to hold payload of the message as soon as it comes into the callback
byte r, g, b;
int status = WL_IDLE_STATUS; // the Wifi radio's status
// set this if you're plugging a BlinkM directly into an Arduino,
// into the standard position on analog in pins 2,3,4,5
// otherwise you can set it to false or just leave it alone
const boolean BLINKM_ARDUINO_POWERED = false;

#define blinkm_addr 0x00 // Default address for the BlinkM
WiFiClient wfClient;
PubSubClient client(server, 1883, callback, wfClient);

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
  
  // Loop until we find a WiFi shield.
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    while(true);
  }
  
  int retryCount = 0;
  // Loop until we are connected to the WiFi network.
  while (status != WL_CONNECTED && retryCount++ < 5) {
    Serial.print("Attempting to connect to WPA SSID: ");
    Serial.println(ssid);
    status = WiFi.begin(ssid, pass);
    Serial.print(".");
    delay(5000);
  }
  
  if( status !=  WL_CONNECTED ){
    Serial.print("Unable to connect to Wifi Network. Please make sure your network is broadcasting and your password is correct.");
  }else{
    Serial.println("You're connected to the network");
    
    printWifiData();
    printCurrentNet();
  
    if (!client.connected()) {
    
      Serial.print("Connecting to MQTT server");
      
      if (client.connect(clientName)) {
        Serial.println("Connected to MQTT server");
        
        Serial.print("Attempting to Subscribe to topic: ");
        Serial.println(topicName);
        
        if(client.subscribe(topicName)){
          Serial.println("Successfully subscribed to topic");
          
          setupBlinkM();
          
          Serial.println("Everything was configured succesfully");
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

void setupBlinkM(){
  Serial.println("Setting up BlinkM light");
  
  if( BLINKM_ARDUINO_POWERED ) {
    BlinkM_beginWithPower();
  } 
  else {
    BlinkM_begin();
  }
  
  delay(100);  // wait for power to stabilize
  BlinkM_stopScript(blinkm_addr); 
  
  int i = 0;
  while(i++<3){
    setColor("Away");
    delay(500);
    setColor("");
    delay(500);
 }
  
  Serial.println("BlinkM light all setup");
}

void setColor(String color)
{
  r = 0;
  g = 0;
  b = 0;
  
  // TODO make this a better switch/case
  if( color != ""){ // Just short cut if we are resetting.
    if(color == "Busy" || color == "DoNotDisturb"){
      r = 255;
    }else if(color == "Away" || color == "TemporarilyAway"){
      r = 255, g = 128;  
    }else if (color == "Free"){
      g = 255;
    }
  }
  
  BlinkM_setRGB(blinkm_addr, r, g, b);
}

void printWifiData() {
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);
  byte mac[6];
  WiFi.macAddress(mac);
  
  Serial.print("MAC address: ");
  Serial.print(mac[5],HEX);
  Serial.print(":");
  Serial.print(mac[4],HEX);
  Serial.print(":");
  Serial.print(mac[3],HEX);
  Serial.print(":");
  Serial.print(mac[2],HEX);
  Serial.print(":");
  Serial.print(mac[1],HEX);
  Serial.print(":");
  Serial.println(mac[0],HEX);
}

void printCurrentNet() {
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());
  byte bssid[6];
  WiFi.BSSID(bssid);
  Serial.print("BSSID: ");
  Serial.print(bssid[5],HEX);
  Serial.print(":");
  Serial.print(bssid[4],HEX);
  Serial.print(":");
  Serial.print(bssid[3],HEX);
  Serial.print(":");
  Serial.print(bssid[2],HEX);
  Serial.print(":");
  Serial.print(bssid[1],HEX);
  Serial.print(":");
  Serial.println(bssid[0],HEX);
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.println(rssi);
  byte encryption = WiFi.encryptionType();
  Serial.print("Encryption Type:");
  Serial.println(encryption,HEX);
}
