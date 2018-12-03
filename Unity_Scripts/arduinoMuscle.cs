using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
//These are the libraries we need to read from the arduino

public class arduinoMuscle : MonoBehaviour {

    SerialPort sp = new SerialPort("COM5", 9600); // This tells the Computer where to listen for Arduino data

    Color lerpedColour = Color.white; // This sets the colour of no flex to White.

    void Start ()
    { // This is a Unity method that allows you to set up things at the start.
        sp.Open(); // This opens or connects the computer to the COM port which then connects to the Arduino for Serial Communication.
        sp.ReadTimeout = 1; // This sets the time for when a code must execute, or else it will create a timeout error: 1 milisecond is set here.
    }
	
	void Update ()
    { // This is a Unity method that allows you to 'update' (change) the state of objects in your scene, once per frame.

        if (sp.IsOpen) // This is a conditional statement. Checks to see if the arduino and computer are communicating to each other.
        {
            try // This is known as a try-catch block. It executes code in a safe environment, and if it encounters any errors it handles them.
            {
                print(float.Parse(sp.ReadLine())); // Getting the current value of the muscle flex as a decimal (real/float) number, printing it out to the console.

                lerpedColour = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, float.Parse(sp.ReadLine()))); // A linear interpolation (lerp) of white (no flex - 0) to red (muscle flexed - 1), mapped to the arduino data.
                
                gameObject.GetComponent<Renderer>().material.color = lerpedColour; // Assigning this lerped colour to the cube to be rendered.
            }
            catch (System.Exception) // This is the catch part, it 'catches' any unexpected errors or exceptions that may have occurred and deals with them.
            {

            }
        }


    }
}
