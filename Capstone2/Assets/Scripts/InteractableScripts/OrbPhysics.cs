using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("hit groumd");
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        }
    }
}
