using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public Transform respawnLocation;
	public delegate void OnRespawn ();
	public OnRespawn onRespawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void respawn(){
		GetComponent<PlayerController> ().StopMovement ();
		transform.position = respawnLocation.transform.position;
		GetComponent<HP> ().AddHealth (GetComponent<HP> ().MaxHealth);
		GetComponent<PlayerController> ().CanMove = true;
		GetComponent<PlayerController> ().enabled = true;

		if (onRespawn != null)
			onRespawn.Invoke ();
	}
}
