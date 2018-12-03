using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
//These are the libraries we need to read from the arduino

public class arduinoJoy : MonoBehaviour {

    public float speed; // A Float is a real number. This float defines how fast the cube in your scene will move.
    private float amountToMove;   // This is a private float, it can only be edited in code. This Float dictates how much the cube should move by, upon detection of a tilt/movement.

    SerialPort sp = new SerialPort("COM5", 9600); // This tells the Computer where to listen for Arduino data
    
    void Start () // This is a Unity method that allows you to set up things at the start.
    {  
        sp.Open(); // This opens or connects the computer to the COM port which then connects to the Arduino for Serial Communication.
        sp.ReadTimeout = 1; // This sets the time for when a code must execute, or else it will create a timeout error: 1 milisecond is set here.

    }

	void Update ()
    { // This is a Unity method that allows you to 'update' (change) the state of objects in your scene, once per frame.
        amountToMove = speed * Time.deltaTime; // This calculates the amountToMove using the formula: amountToMove = speed (The constant, base speed of how fast the cube should move) multiplied by the change in Time (deltaTime) to accurately get the time before and after the change occurs.
        transform.Rotate(100 * Time.deltaTime, 0, 0); // This continously rotates the cube on its X-axis at 100 units.

        if (sp.IsOpen) // This is a conditional statement. Checks to see if the arduino and computer are communicating to each other.
        {
            try // This is known as a try-catch block. It executes code in a safe environment, and if it encounters any errors it handles them.
            {
                MoveObject(sp.ReadByte());  // This executes the MoveObject method. It is using the arduino's readings to determine which direction to move the cube in.
            }
            catch (System.Exception) // This is the catch part, it 'catches' any unexpected errors or exceptions that may have occurred and deals with them.
            {

            }
        }
	}

    void MoveObject(int Direction) // This is a custom method to tell Unity, which direction the cube should move in.
        /* In the parantheses, the Integer Threshold is a direct reference to the Arduino. If you look at the Arduino script you would see that we have serial.write() functions.
         * This threshold takes in the serial.write() function and sees its value.
        */
    {
        if (Direction == 1) // If the joystick is stationary.
        { // Dont do anything.

           // transform.Translate(Vector3.forward * 0, Space.World);
           // transform.localScale += new Vector3(amountToMove, 0, 0); 

        }

        if (Direction == 2) // If the joystick is tilted to the right.
        { // Move the object to the right / grow the cube along the x-axis.

            // transform.Translate(Vector3.right * amountToMove, Space.World);
            transform.localScale += new Vector3(amountToMove, 0, 0);
        }

        if (Direction == 3) // If the joystick is tilted to the left.
        { // Move the cube to the left / grow the cube along the y-axis.
            // transform.Translate(Vector3.left * amountToMove, Space.World);
            transform.localScale += new Vector3(0, amountToMove, 0);
        }

        if (Direction == 4) // If the joystick is tilted downwards.
        { // Move the cube backwards / grow the cube along the z-axis.
            // transform.Translate(Vector3.back * amountToMove, Space.World);
            transform.localScale += new Vector3(0, 0, amountToMove);
        }

        if (Direction == 5) // If the joystick is tilted up.
        { // Move the cube forward.
            transform.Translate(Vector3.forward * amountToMove, Space.World);
        }
    } 
}
