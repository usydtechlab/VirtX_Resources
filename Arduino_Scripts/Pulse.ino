/*
 * To measure the BPM (Beats Per Minute) from the Pulse Sensor that is connected to an Arduino.
 * The Pulse Sensor is a green LED (Light Emitting Diode) and a photoresistor.
 * The sensor measures the change in volume of blood that is flowing under your skin.
 * The sensor must be placed evenly over the skin, in order to get a moderately accurate, valid and reliable reading.
 */

// Constant Integers (Whole numbers) that relate to certain 'pins' (places) on the Arduino.
const int pulsePin = A0;
const int outputPin = 13;

// Dynamic (Changing) Integers that change when something is done in the setup() {} or loop() {} enclosures.
int sensorValue = 0;
int outputValue = 0;

// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {

  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);
}

// Runs continiously, on loop.
void loop() {

  // This uses a function called 'analogRead': This basically reads in the value of the Sensor (Getting the RAW data).
  sensorValue = analogRead(pulsePin);

  // This 'maps' (interprets) the values obtained from the RAW data, to values between the average Human BPM (60 - 150).
  outputValue = map(sensorValue, 0, 1024, 50, 150);

  // This uses a function called 'analogWrite': It 'writes' or records the mapped data to the output Integer pin.
  analogWrite(outputPin, outputValue);

  // This communicates (transfers) the data from the arduino to the computer, to print out the value of the mapped BPM.
  Serial.println(outputValue);

  // UNITY CODE: TO interact with the Oculus Rift, we write a specific integer so that Unity can detect when the sensor is reading a heartrate and to react to it.
  if (outputValue > 60) { // This checks if the heartrate is greater than 60 to be in a 'normal' range.
    Serial.write(2); // Say to the computer: 2.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay to keep things from not overlapping each other.
  }
}
