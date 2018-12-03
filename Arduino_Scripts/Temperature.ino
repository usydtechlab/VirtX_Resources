/*
 * To use a Thermister sensor and convert its readings into degrees celsius.
 * And display this result in terms of colours on a Cube in VR, using Oculus Rift and Unity.
 */


double Thermister(int rawValue) { // A custom function that returns (outputs) the temperature from the raw value to celsius, as a decimal number.

  // Maths done to convert raw value from the sensor to degrees in celsius. * = multiplying.
  double celsius;
  celsius = log(((10240000 / rawValue) - 10000));
  celsius = 1 / (0.001129148 + (0.000234125 + (0.0000000876741 * celsius * celsius )) * celsius );
  celsius = celsius - 273.15 - 10;

  // Output the celsius conversion.
  return celsius;
}

// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {
  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);
}

// Runs continiously, on loop.
void loop() { 
  double temp = Thermister(analogRead(A0)); // Assigning the maths calculation to 'temp' which is a decimal/real number.
  Serial.println(temp); // Printing out the celsius temperature from the sensor.
}
