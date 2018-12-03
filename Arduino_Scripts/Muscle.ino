/*
 * To measure the muscle flex of the forearm, from a flex sensor that is connected to an Arduino.
 * This muscle flex sensor 'MyoWare' measures the filtered and rectified electrical activity of a muscle.
 * Gets the electrical activity of a muscle and produces an analogue output signal that can be read by the Arduino.
 */

// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {
  
  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);
}

// Runs continiously, on loop.
void loop() {
  
  // Read the input on analog pin 0, which is the muscle flex sensor:
  int sensorValue = analogRead(A0);
  
  // Convert the analog reading (which goes from 0 - 1023) to a voltage (0 - 5V)
  float voltage = sensorValue * (5.0 / 1023.0);
  
  // Print out the value you read:
  Serial.println(voltage);
}
