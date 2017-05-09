#include <CmdMessenger.h> // CmdMessenger
#include <Adafruit_NeoPixel.h>

String color = "";

// Attach a new CmdMessenger object to the default Serial port
CmdMessenger cmdMessenger = CmdMessenger(Serial);

#define PIN 15    // Pin for data
#define LEDS 32   // How many LEDs?
#define ROWS 4    // count LEDs downward
#define COLUMS 8  //Count your LEDS to the right
#define SPEED 100 //faster the lower the number

// Parameter 1 = number of pixels in strip
// Parameter 2 = Arduino pin number (most are valid)
// Parameter 3 = pixel type flags, add together as needed:
//   NEO_KHZ800  800 KHz bitstream (most NeoPixel products w/WS2812 LEDs)
//   NEO_KHZ400  400 KHz (classic 'v1' (not v2) FLORA pixels, WS2811 drivers)
//   NEO_GRB     Pixels are wired for GRB bitstream (most NeoPixel products)
//   NEO_RGB     Pixels are wired for RGB bitstream (v1 FLORA pixels, not v2)
//   NEO_RGBW    Pixels are wired for RGBW bitstream (NeoPixel RGBW products)
Adafruit_NeoPixel strip = Adafruit_NeoPixel(LEDS, PIN, NEO_GRB + NEO_KHZ800);

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

    if (color == "Busy" || color == "DoNotDisturb")
    {
        setColor(strip.Color(255, 0, 0));
    }
    else if (color == "Away" || color == "TemporarilyAway")
    {
        setColor(strip.Color(255, 180, 0));
    }
    else if (color == "Free")
    {
        setColor(strip.Color(0, 255, 0));
    }
    else
    {
        setColor(strip.Color(0, 0, 0));
    }
}



void setup()
{
    Serial.begin(115200);

    // Adds newline to every command
    cmdMessenger.printLfCr();

    // Attach my application's user-defined callback methods
    attachCommandCallbacks();

    strip.begin();
    strip.show(); // Initialize all pixels to 'off'
}

// Loop function
void loop()
{
    // Process incoming serial data, and perform callbacks
    cmdMessenger.feedinSerialData();
}

// Sets Solid Color
void setColor(uint32_t c)
{
    for (uint16_t i = 0; i < strip.numPixels(); i++)
    {
        strip.setPixelColor(i, c);
    }
    strip.show();
}
