using UnityEngine;
using System.Collections;

public class sensorForWallTimer : MonoBehaviour {

    public GameObject gate, redLight;

    GameObject thing;
    float waitLength;
    float waitTime;
    float lightTimer;
    float disableTime;
    bool AEVinArea, disabled;


	// Use this for initialization
	void Start () {
        waitLength = 6.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (thing)
        {
            AEVinArea = gameObject.GetComponent<Collider>().bounds.Intersects(thing.gameObject.GetComponent<Collider>().bounds);
        }
        if (disabled)
        {
            if(Time.time - disableTime > 10)
            {
                disabled = false;
            }
            waitTime = Time.time;
        }
        else if (AEVinArea)
        {
            if(Time.time - waitTime > waitLength)
            {
                //tell gate to open
                gate.BroadcastMessage("open");
                disabled = true;
                disableTime = Time.time;
            }
            if(Time.time - lightTimer > 0.5f)
            {
                redLight.gameObject.SetActive(!redLight.gameObject.activeInHierarchy);
                lightTimer = Time.time;
            }
        }
        if (!AEVinArea)
        {
            redLight.gameObject.SetActive(true);
            waitTime = Time.time;
        }
	}

    void OnTriggerEnter(Collider c)
    {
        thing = c.gameObject;
        AEVinArea = true;
        waitTime = Time.time;
    }

    void disable()
    {
        disabled = true;
        disableTime = Time.time;
    }
}
