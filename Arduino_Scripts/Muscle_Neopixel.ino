#include <Adafruit_NeoPixel.h> // Inclduing the NeoPixel library to set up the strip of LEDs.

int musclePin = A0; // Referencing the analogue pin on the Arduino that holds the Muscle Sensor.
int neoPin = 6; // Referencing the digital pin on the Arduino that holds the Neopixel strip.

const int NUMPIXELS = 10; // Telling the arduino how many 'pixels' are on the strip attached to it.
Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUMPIXELS, neoPin, NEO_GRB + NEO_KHZ800); // Constructing the neopixel strip object.

// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {

  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);
  
  strip.begin();
  for (int i = 0; i < NUMPIXELS; i++) {
    strip.setPixelColor(i, strip.Color(0, 0, 0));
  }
  strip.setBrightness(50);
  strip.show();
}

void loop() {
  uint32_t magenta = strip.Color(255, 0, 255); // A special integer that is for storing colours. This is storing the colour Magenta in rgb values (R = 255, G = 0 , B = 255).

  /* 
   * 1) Read the input on analog pin 0, which is the muscle flex sensor.
   * 2) Convert the analog reading (which goes from 0 - 1023) to a voltage (0 - 5V)
   * 3) Print out the value you read.
  */
  int sensorValue = analogRead(musclePin);
  float muscleLevel = sensorValue * (5.0 / 1023.0);
  Serial.print(muscleLevel);

  int neoValue = map( muscleLevel, 0, 3, 0, 10 ); // Mapping the values read from the Muscle Sensor (refactoring the range so that it is easier to convert) to the number of pixels on the Neopixel strip.
  Serial.print("   ");
  Serial.println(neoValue); // Printing out the mapped neoValue.

  strip.clear(); // On the start of every time the program executes, clear all of the lit LEDs.
  if (neoValue == 0) { // If there is no muscle contraction (no sensor reading).
    strip.clear(); // Ensure that all of the LEDs are still cleared
  } else {
    strip.fill(magenta, 0, neoValue); // Filling a range of the pixels on the string with the colour magenta, based on the muscle contraction.
  }

  strip.show(); // Update the strip to show the lit pixels.

  delay(20); // Add a small delay to prevent overload.

}
