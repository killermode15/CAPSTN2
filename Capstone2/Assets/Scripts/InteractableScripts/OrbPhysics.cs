using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponentInChildren<FloatingObject>().enabled = false;
        transform.GetComponentInChildren<SphereCollider>().isTrigger = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("hit groumd");
            transform.GetComponentInChildren<FloatingObject>().enabled = false;
            transform.GetComponentInChildren<SphereCollider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
