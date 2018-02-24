using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPrefabDmgTest : MonoBehaviour {

	public float Damage;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.GetComponent<HP> ().RemoveHealth (Damage);
		}
	}
}
