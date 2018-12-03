/*
 * To use a Joystick, similar to what you would see on a Video Game Console Controller (think the Xbox or PlayStation). 
 * Interact with an object in Virtual Reality, with this Joystick. By detecting the movement of the joystick.
 */
 
// Constant Integers (Whole numbers) that relate to certain 'pins' (places) on the Arduino.
const int VRx = A0;
const int VRy = A1;
const int SW = 5;

// Runs at the beginning of the 'sketch' (program). And only runs once, initially.
void setup() {
  
  // Opens a connection to the Serial Port of the Arduino, to communicate with it.
  Serial.begin(9600);

  // These 3 lines use a function called pinMode which tells all of the above places on the Arduino to take in data from the Joystick.
  pinMode(VRx, INPUT);
  pinMode(VRy, INPUT);
  pinMode(SW, INPUT);

  // These 3 lines check to see if the joystick is working.
  digitalWrite(SW, HIGH);
  analogRead(VRx);
  analogRead(VRy);

  /* Read and Writing with Arduinos: 
   * An Arduino takes in data and can output data.
   *  The process of taking in data, is known as Reading Data. This can be data being read from sensors.
   *  We use the function: analogRead() to read in data from ANALOG (continous) sensors/devices OR digitalRead() to read in data FROM BINARY/DIGITAL (discrete) sensors/devices.
   *  An example of an analog device would be a heart rate monitor (pulse sensor) and an example of a digital device would be a button.
   *  We can also see what the Arduino is reading in through Serial Communication between itself and the computer using serial.println().
   *  
   *  We can also WRITE (output) data from the Arduino. This can be directly from code or a device that has a pinMode of OUTPUT.
   *  An example would be an LED that could light up when something is detected, we can also write to the Serial Monitor (An SMS like feature between the Arduino and the Computer).
   *  These are known as Write methods. 
   *  
   *  Write Methods:
   *    Serial.write() => Say something to the computer.
   *    digitalWrite() => Output something to a digital device.
   *    analogWrite() => Output something to an analogue device.
   *    
   *  Read Methods:
   *    Serial.println() => Get data input from the Arduino.
   *    digitalRead() => Read data from a digital device.
   *    analogRead() => Read data from an analogue device.
  */

}

// Runs continiously, on loop.
void loop() {
  /*
   * UNITY CODE: To interact with Unity and the Oculus Rift.
   * We check how the Joystick is moving (Up, Down, Left, Right or being pressed) and dictate this in the form of a number.
   * This number is then transferred to Unity in order to tell the cube which direction to move in.
   */

  // This checks if the joystick is being pressed down.
  if (digitalRead(SW) == LOW) { // PRESSING IT DOWN: Direction 11.
    Serial.write(11); // Say to the computer: 11.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }

  // This checks if the joystick is stationary, not being pressed/moved at all.
  if ( (analogRead(VRx) >= 440 && analogRead(VRx) <= 540) || (analogRead(VRy) >= 457 && analogRead(VRy) <= 550)) { // STATIONARY: Direction 1.
    Serial.write(1); // Say to the computer: 1.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }

  // This checks if the joystick is being tilted (moved) to the right.
  if (analogRead(VRx) >= 0 && analogRead(VRx) < 440) { // MOVING RIGHT: Direction 2.
    Serial.write(2); // Say to the computer: 2.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }

  // This checks if the joystick is being tilted (moved) to the left.
  if (analogRead(VRx) <= 1023 && analogRead(VRx) > 540) { // MOVING LEFT: Direction 3.
    Serial.write(3); // Say to the computer: 3.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }

  // This checks if the joystick is being tilted (moved) down.
  if (analogRead(VRy) >= 0 && analogRead(VRy) < 457) { // MOVING DOWN: Direction 4.
    Serial.write(4); // Say to the computer: 4.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }

  // This checks if the joystick is being tilted (moved) up.
  if (analogRead(VRy) <= 1023 && analogRead(VRy) > 550) { // MOVING UP: Direction 5.
    Serial.write(5); // Say to the computer: 5.
    Serial.flush(); // Clean/erase all excess or unwanted data.
    delay(20); // Add a 20 millisecond delay, to keep things from not overlapping each other.
  }
}
