using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
//These are the libraries we need to read from the arduino

public class arduinoTemp : MonoBehaviour {

    SerialPort sp = new SerialPort("COM5", 9600); // This tells the Computer where to listen for Arduino data

    void Start ()
    { // This is a Unity method that allows you to set up things at the start.
        sp.Open(); // This opens or connects the computer to the COM port which then connects to the Arduino for Serial Communication.
        sp.ReadTimeout = 1; // This sets the time for when a code must execute, or else it will create a timeout error: 1 milisecond is set here.
    }
	
	void Update () {    // This is a Unity method that allows you to 'update' (change) the state of objects in your scene, once per frame.

        if (sp.IsOpen) // This is a conditional statement. Checks to see if the arduino and computer are communicating to each other.
        {
            try // This is known as a try-catch block. It executes code in a safe environment, and if it encounters any errors it handles them.
            {
                print(sp.ReadLine()); // Getting the current value of the muscle flex as a decimal (real/float) number, printing it out to the console.

                changeColour(float.Parse(sp.ReadLine())); // Changes colour of the cube based on the temperature sensor reading.

            }
            catch (System.Exception) // This is the catch part, it 'catches' any unexpected errors or exceptions that may have occurred and deals with them.
            {

            }
        }
    }

    void changeColour(float temperature) // This is a custom method to tell Unity, what colour the cube should be.
       /* In the parantheses, the Integer temperature is a direct reference to the Arduino. If you look at the Arduino script you would see that we have serial.write() functions.
        * This temperature takes in the serial.write() function and sees its value.
        */
    {   // Temperature readings are in celsius.
        if (temperature > 25) // Change the colour of the cube to red. Red = It's hot.
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (temperature > 20 && temperature < 25) // Change the colour of the cube to yellow. Yellow = It's nice.
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (temperature > 16 && temperature < 20) // Change the colour of the cube to green. Green = It's cool.
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }

        if (temperature < 15) // Change the colour of the cube to cyan. Cyan = It's cool.
        {
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
}
