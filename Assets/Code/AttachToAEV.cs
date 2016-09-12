using UnityEngine;
using System.Collections;

public class AttachToAEV : MonoBehaviour {

    SpringJoint sj;
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<SpringJoint>();
        sj = gameObject.GetComponent<SpringJoint>();
	}
	
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "AEV" && sj)
        {       
            sj.connectedBody = c.gameObject.GetComponent<Rigidbody>();
            sj.spring = 2500;
            sj.damper = 500;
            sj.maxDistance = 0.025f;
            sj.enableCollision = true;          
        }
    }
}
