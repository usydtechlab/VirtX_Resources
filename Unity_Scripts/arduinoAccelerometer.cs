using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
// These are the libraries we need to read from the Arduino.

public class arduinoAccelerometer : MonoBehaviour {

    public float speed; // A public float that dictates the speed of the rotation of the cube.
    public int arrLen; // A public float that dictates the sample size to average out the accelerometer values (smoothing them).

    private SerialPort serialPort; // A private SerialPort, only available in the code. To set up the Serial Port to communicate with the Computer.
    private bool serialOK = false; // A boolean (is it on or off) to see if the serial communication is on/off.

    private Vector3 nextRot; // A private Vector3 (X, Y, Z) to get the specific values of the accelerometer's readings. 

    private Vector3[] angleBuffer; // A 'buffer' to keep the accelerometer's values consistent.
    private int bufIndex = 0; // An index to loop through the array [collection] of the buffer, to keep the readings consistent.

    void Start()  // This is a Unity method that allows you to set up things at the start.
    {

        serialPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One); // This tells the Computer where to listen for Arduino data.
        angleBuffer = new Vector3[arrLen]; // This sets the angleBuffer array [ a collection of angles ] to collect.

        try  // This is known as a try-catch block. It executes code in a safe environment, and if it encounters any errors it handles them.
        { // Setting up the serial port communication.
            serialPort.RtsEnable = true;
            serialPort.Open();
            serialOK = true;
            Debug.Log("Serial OK");
        }
        catch (Exception)  // This is the catch part, it 'catches' any unexpected errors or exceptions that may have occurred and deals with them.
        {
            Debug.LogError("Failed to open serial port for accelero-sensor.");
        }
    }

    void ReadSerial() // A custom function that deals with the incoming data from the Arduino/Accelerometer.
    {
        string dataString = serialPort.ReadLine(); // Get the current line of the data input.
        var dataBlocks = dataString.Split(','); // Breaks up the line of the data input into X, Y and Z values.

        if (dataBlocks.Length < 3) // If not all of the data is there, then stop everything and exit.
        {
            Debug.LogWarning("Invalid data received");
            return;
        }

        int angleX, angleY, angleZ; // Setting up the angles

        // These if statements check if the values are numeric values and not alphabetical/alphanumeric or contains other letters.
        if (!int.TryParse(dataBlocks[0], out angleX)) 
        {
            Debug.LogWarning("Failed to parse angleX. RawData: " + dataBlocks[0]);
            return;
        }
        if (!int.TryParse(dataBlocks[1], out angleY))
        {
            Debug.LogWarning("Failed to parse angleY. RawData: " + dataBlocks[1]);
            return;
        }
        if (!int.TryParse(dataBlocks[2], out angleZ))
        {
            Debug.LogWarning("Failed to parse angleZ. RawData: " + dataBlocks[2]);
            return;
        }

        SetRotation(angleX, angleZ, angleY); // Calling another function
    }


    void SetRotation(int x, int y, int z) // Another custom function that takes in:
        /* 
         * @Param: X - angle on the x axis.
         *         Y - angle on the y axis.
         *         Z - angle on the z axis.
         *         
         * @Action: Sets the rotation of the cube for the next frame.
        */
    {
        Vector3 newRot = new Vector3((float)x, (float)y, (float)z); // Sets the new rotation as a Vector.
        if (bufIndex < arrLen - 1) // Increases the collection of angles, if its full.
        {
            angleBuffer[bufIndex] = newRot;
            bufIndex++;
        }

        else
        {   
            // Create a new entry in the angleBuffer for the new rotation to show in the frame.

            var newArray = new Vector3[angleBuffer.Length]; // A temporary collection of angles to enter in the new set of values.
            Array.Copy(angleBuffer, 1, newArray, 0, angleBuffer.Length - 1); // Copying the original collection.
            newArray[angleBuffer.Length - 1] = newRot;

            angleBuffer = newArray;

            float X = 0f, Z = 0f;

            for (int i = 0; i < angleBuffer.Length; i++) // Adding the angles to the array.
            {
                X += angleBuffer[i].x;
                Z += angleBuffer[i].z;
            }

            X /= (float)angleBuffer.Length;
            Z /= (float)angleBuffer.Length;

            // Applying this rotation to the next frame.
            nextRot = new Vector3(X, 0f, Z);
        }
    }

    void Update() // This is a Unity method that allows you to 'update' (change) the state of objects in your scene, once per frame.
    {
        if (serialOK) // This is a conditional statement. Checks to see if the arduino and computer are communicating to each other.
        {
            try
            {
                ReadSerial(); // Executes the ReadSerial Function.
            }
            catch (Exception)
            {
                Debug.LogWarning("Serial Failed"); // Or else print this error out: Serial Failed!
            }
        }

        // Perform the rotation!
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(nextRot), Time.deltaTime * speed);
    }
}
