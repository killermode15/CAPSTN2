using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunElement : MonoBehaviour {

	public float StunDuration;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			StateManager sm = other.gameObject.GetComponent<StateManager> ();
			if (!sm.CompareToCurrentState (typeof(StunnedState))) {
				sm.ChangeState (sm.GetState ("StunnedState"));
				other.gameObject.GetComponent<StunnedState> ().stunnedDuration = StunDuration;
			} else {

			}
			Destroy (this.gameObject);
		}
	}
}
