/*
 * To use an accelerometer and rotate a cube in VR with it.
 */

// Constant Integers (Whole numbers) that relate to certain 'pins' (places) on the Arduino.
const int zAxis = A2;
const int yAxis = A1;
const int xAxis = A0;

// Integers to get the range of raw values for each axis: X, Y and Z,
int zHigh = 419, zLow = 283,
        xHigh = 399, xLow = 264,
        yHigh = 396, yLow = 260;
        
// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {
  
  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);
}

// Runs continiously, on loop.
void loop() {

  // Shorts (decimal numbers) that map the raw values to angles between -90 degrees and 90 degrees for each axis.
  short angleX = map( constrain (  analogRead(xAxis), xLow, xHigh ), xLow, xHigh, -90, 90  );
  short angleY = map( constrain (  analogRead(yAxis), yLow, yHigh ), yLow, yHigh, -90, 90  );
  short angleZ = map( constrain (  analogRead(zAxis), zLow, zHigh ), zLow, zHigh, -90, 90  );

  // Creating the data string to pass to the computer in the serial.
  String dataString = String(angleX);

  dataString += ",";
  dataString += String(angleY); // Adding the y axis to the string.
  dataString += ",";
  dataString += String(angleZ); // Adding the z axis to the string.

  Serial.println(dataString); // Say to the computer: These are the axis angles: X = , Y = , Z = .

}
