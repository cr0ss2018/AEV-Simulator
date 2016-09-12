using UnityEngine;
using System.Collections;

public class OpenGate : MonoBehaviour {

    Vector3 startPos, endPos;
    bool raised;
    bool raising;
    bool lowering;
    float startTime;
    float journeyLength;
    float speed;
    // Use this for initialization
    void Start () {
        startPos = transform.position;
        endPos = transform.position - transform.forward * 10;
        journeyLength = Vector3.Distance(startPos, endPos);
        speed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (raising)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fracJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
            if(fracJourney >= 1)
            {
                raising = false;
                raised = true;
                startTime = Time.time;
            }
        }
        else if (raised)
        {
            if(Time.time - startTime > 7)
            {
                raised = false;
                lowering = true;
                startTime = Time.time;
            }
        }
        else if(lowering)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fracJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(endPos, startPos, fracJourney);
            if (fracJourney >= 1)
            {
                lowering = false;
            }
        }
	}

    void open()
    {
        if (!raised && !lowering && !raising)
        {
            startTime = Time.time;
            raising = true;
        }        
    }
}
