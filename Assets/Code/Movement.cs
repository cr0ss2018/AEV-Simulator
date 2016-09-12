using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    Vector3 startPos, rescueAEVstartPos;
    Quaternion startRot, rescueAEVstartRot;
    float throttle;
    float maxThrottle;
    float staticFriction, kineticFriction;
    float scalar;
    float positionScalar;
    GameObject rescueAEV;
    GameObject propeller;
    GameObject frontWheel, rearWheel;
    Rigidbody rb;


    // Use this for initialization
    void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
        rescueAEV = GameObject.FindGameObjectWithTag("RescueAEV");
        rescueAEVstartPos = rescueAEV.transform.position;
        rescueAEVstartRot = rescueAEV.transform.rotation;
        maxThrottle = 100f;
        staticFriction = 0.25f;
        kineticFriction = 0.15f;
        scalar = 1f;
        positionScalar = 170;
        rb = transform.gameObject.GetComponent<Rigidbody>();
        propeller = GameObject.FindGameObjectWithTag("propeller");
        frontWheel = GameObject.FindGameObjectWithTag("frontWheel");
        rearWheel = GameObject.FindGameObjectWithTag("rearWheel");

    }

    // Update is called once per frame
    void FixedUpdate () {
        Debug.Log(throttle);
        getInput();
        spinPropeller();
        spinWheels();
        updateMovement();
    }
    void updateMovement()
    {        
        rb.AddForce((-transform.up) * (throttle / scalar));
        //transform.position += (-transform.up) * (throttle / positionScalar);
    }
    
    void getInput()
    {
        if (Input.GetKey("r"))
        {
            throttle = 0;
            transform.position = startPos;
            transform.rotation = startRot;
            rb.velocity = Vector3.zero;

            rescueAEV.transform.position = rescueAEVstartPos;
            rescueAEV.transform.rotation = rescueAEVstartRot;
            rescueAEV.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rescueAEV.GetComponent<SpringJoint>().connectedBody = null;
        }
        if (Input.GetKey("q"))
        {
            throttle = 0;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            throttle += Input.GetAxis("Vertical");
            if(throttle > maxThrottle)
            {
                throttle = maxThrottle;
            }
            else if(throttle < -maxThrottle)
            {
                throttle = -maxThrottle;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();        
        }
    }

    void spinPropeller()
    {
        propeller.transform.RotateAround(propeller.transform.position - 
            (propeller.transform.right *1.165f)  + (propeller.transform.up * 1.575f),
            propeller.transform.forward, throttle);
    }
    void spinWheels()
    {       
            if (frontWheel)
            {
                frontWheel.transform.RotateAround(frontWheel.transform.position
                    + frontWheel.transform.forward * 1.82f + frontWheel.transform.up * 5.9f,
                    frontWheel.transform.right, -rb.velocity.magnitude * Mathf.Clamp(throttle, -1, 1));

            }
            if(rearWheel)
            {
            rearWheel.transform.RotateAround(rearWheel.transform.position
                    + rearWheel.transform.forward * 5.55f + rearWheel.transform.up * 5.9f,
                    rearWheel.transform.right, -rb.velocity.magnitude * Mathf.Clamp(throttle, -1, 1));
            }
        
    }
}
