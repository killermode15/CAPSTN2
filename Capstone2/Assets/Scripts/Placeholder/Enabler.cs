using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour {

	Vector3 initialSpawn;
	public GameObject Object;
	// Use this for initialization
	void Start () {
		initialSpawn = transform.position;
		DisableCollder ();
	}
	
	// Update is called once per frame
	void Update () {
		//float distance = Vector3.Distance (transform.position, initialSpawn);
		//if (distance >= 1.5f) {
		//	EnableCollider ();
		//}
	}

	void EnableCollider(){
		GetComponent<BoxCollider> ().enabled = true;
	}

	void DisableCollder(){
		GetComponent<BoxCollider> ().enabled = false;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Debug.Log("PLAYER LEFT");
			EnableCollider();
		}
	}
}
