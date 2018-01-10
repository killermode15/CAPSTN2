using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	float capacity;

	void Start(){
		capacity = transform.parent.gameObject.GetComponent<PlayerDefendSkill> ().Capacity;
	}

	void OnCollisionEnter(Collision other){
		//negate damage(?)
	}
}
