using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
//These are the libraries we need to read from the arduino

public class arduinoPulse : MonoBehaviour {
    public bool rotateOn;                   // This puts a button on the screen so we can turn rotation on and off without writing code!
    public float approachSpeed = 0.02f;     // A Float is a real number. This float defines how fast the model in your scene will grow or shrink
    public float growthBound = 2f;          // This Float dictates the maximum size of your model
    public float shrinkBound = 1f;          // This Float dictates the minimum size of your model 

    //Create a List of new Dropdown options for COM port, coming soon for productionisation
    // public enum ComPort
    // {
    //    COM1, COM2, COM3, COM4, COM5, COM6
    // }
    //
    // public class AClass : MonoBehaviour
    // {
    //   public ComPort current;

    // }
    //public ComPort ComPorts;

    public SerialPort sp = new SerialPort("COM5", 9600); // This tells the Computer where to listen for Arduino data

    private float amountToPulse;  // This is a private float, it can only be edited in code. This Float dictates how much the Heart should grow by, upon detection of a heartbeat.
    private float currentRatio = 1; // This is a private float, it can only be edited in code. This Float dictates the current size of the Heart as a ratio between the growthBound and the shrinkBound.
    private Coroutine routine; // This is a private 'Co Routine'. (This means, it can run simultaneously with other code at the same time).
      
    
    void Start () { // This is a Unity method that allows you to set up things at the start.
        sp.Open(); // This opens or connects the computer to the COM port which then connects to the Arduino for Serial Communication.
        sp.ReadTimeout = 1; // This sets the time for when a code must execute, or else it will create a timeout error: 1 milisecond is set here.
    }

    void Update () { // This is a Unity method that allows you to 'update' (change) the state of objects in your scene, once per frame.
        amountToPulse = approachSpeed * Time.deltaTime; // This calculates the amountToPulse using the formula: amountToPulse = approachSpeed (The constant, base speed of how fast the model should grow/shrink) multiplied by the change in Time (deltaTime) to accurately get the time before and after the change occurs.

        if (rotateOn) // This is a conditional statement. IF the heart is spinning, look at line 9 above rotateOn. By itself, it evaluates to True.
        {
            transform.Rotate(0, 10 * Time.deltaTime, 0); // This rotates the heart at a flat rate of 10 units.
        }

        if (sp.IsOpen) // This is a conditional statement. Checks to see if the arduino and computer are communicating to each other.
        {
            try // This is known as a try-catch block. It executes code in a safe environment, and if it encounters any errors it handles them.
            {
                print(sp.ReadLine()); // This is for the developer, prints out what the arduino is saying the computer in the Console window.
                this.routine = StartCoroutine(this.Pulse(sp.ReadByte())); // This executes the 'enumerator' method as a CO-ROUTINE (Happening simultaenously). It is using the arduino's readings to determine the BPM.
            }
            catch (System.Exception) // This is the catch part, it 'catches' any unexpected errors or exceptions that may have occurred and deals with them.
            {
  
            }
        }

	}

    IEnumerator Pulse(int Threshold) // This is an IEnumerator. An Enumerator is a continuous function (A block of code that executes) repeatedly.
        /* In the parantheses, the Integer Threshold is a direct reference to the Arduino. If you look at the Arduino script you would see that we have serial.write() functions.
         * This threshold takes in the serial.write() function and sees its value.
         * That is, if the heart is beating in a normal range, then tell the computer 2!
         */
    {
        while (Threshold == 2) // This is a type of loop, that says while the Arduino is saying 2 to the computer (it is beating in a normal range).
        {
            while (this.currentRatio != this.growthBound) // This WHILE loop detects if the currentRatio (size) of the heart DOES NOT EQUAL the upper limit (growthBound).
            {
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed); // Re-calculate the ratio to move to the maximum size, with respect to the approachSpeed given.
                transform.localScale = Vector3.one * currentRatio; // Apply this ratio to the Heart, in order to grow.
                yield return new WaitForEndOfFrame(); // This is a 'generator' method, which ensures that the actions are performed, without anything disrupting it.
            }

            while (this.currentRatio != this.shrinkBound) // This WHILE loop detects if the currentRatio (size) of the heart DOES NOT EQUAL the lower limit (shrinkBound).
            {
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed); // Re-calculate the ratio to move to the smallest size, with respect to the approachSpeed given.
                transform.localScale = Vector3.one * currentRatio; // Apply this ratio to the Heart, in order to shrink.
                yield return new WaitForEndOfFrame(); // This is a 'generator' method, which ensures that the actions are performed, without anything disrupting it.
            }
        }
    }
}