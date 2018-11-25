using UnityEngine;
using System.Collections;


public class OculusGoControllerMovement : MonoBehaviour
{
    public static float standardSpeed = 10.0f;
    public static float fastSpeed = 15.0f;
    public static float rotationSpeed = 60.0f;
    public static float orientation = 0.0f;
    public static float positionalSpeed = 7.5f;
    public float speed = standardSpeed;

    int mindfulPress = 0;
    public GameObject CenterEyeAnchor;

    Vector2 coord = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

    Rigidbody rb;
    private bool keyPressedW, keyPressedA, keyPressedS, keyPressedD;

    // Use this for initialization
    void Start ()
    {   
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        float primaryIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (OVRInput.Get(OVRInput.Button.Back) || OVRInput.Get(OVRInput.Button.DpadDown))
        {
            // rb.velocity = CenterEyeAnchor.transform.forward * speed;
            rb.AddForce(-transform.forward * 31);
        }


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.DpadUp))
        {
            //rb.velocity = CenterEyeAnchor.transform.forward * speed * -1;
            rb.AddForce(transform.forward * 30);
        }


        if (OVRInput.Get(OVRInput.Button.DpadLeft))
        {
            //rb.velocity = CenterEyeAnchor.transform.forward * speed * -1;
            rb.AddForce(-transform.up * 30);
        }


        if (OVRInput.Get(OVRInput.Button.DpadLeft))
        {
            //rb.velocity = CenterEyeAnchor.transform.forward * speed * -1;
            rb.AddForce(transform.right * 30);
        }


        // Left Analog Stick Movement (Camera Face Movement)
        if (primaryAxis.x != 0.0f || primaryAxis.y != 0.0f)
         {
             //rb.velocity = CenterEyeAnchor.transform.forward * speed * primaryAxis.y + CenterEyeAnchor.transform.right * speed * primaryAxis.x;
             rb.velocity = CenterEyeAnchor.transform.forward * speed * primaryAxis.y;
         }
         else if (primaryAxis.x == 0.0f && primaryAxis.y == 0.0f && (OVRInput.Get(OVRInput.Button.DpadUp) == false &&
             OVRInput.Get(OVRInput.Button.DpadDown) == false && OVRInput.Get(OVRInput.Button.DpadRight) == false &&
             OVRInput.Get(OVRInput.Button.DpadLeft) == false) && rb.velocity.magnitude > 0)
         {
             rb.velocity = CenterEyeAnchor.transform.forward * 0.0f + CenterEyeAnchor.transform.right * 0.0f;
         }

         // Triggers Vertical Movement (Game World Vertical Movement)
         if (primaryIndex != 0.0f || secondaryIndex != 0.0f)
         {
             rb.velocity = transform.up * positionalSpeed * (secondaryIndex - primaryIndex);
         }
    }
}
