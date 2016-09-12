using UnityEngine;
using System.Collections;

public class CancelGate : MonoBehaviour {

    public GameObject sensor1;
    GameObject thing;

    void Update()
    {
        if (thing)
        {
            if (thing.gameObject.GetComponent<Collider>().bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
            {
                sensor1.BroadcastMessage("disable");
            }
        }
    }
	
    void OnTriggerEnter(Collider c)
    {
        sensor1.BroadcastMessage("disable");
        thing = c.gameObject;
    }
}
