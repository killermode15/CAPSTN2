using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	float damage;

	void Start(){
		damage = transform.parent.gameObject.GetComponent<PlayerAttackSkill> ().Damage;
	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("i hit an enemy!");
			//other.gameObject.CompareTag("Enemy").HP -= damage; ??????????
		}
	}
}
