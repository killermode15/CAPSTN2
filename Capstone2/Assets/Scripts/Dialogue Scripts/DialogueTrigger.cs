using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	[HideInInspector] public bool triggered;

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			TriggerDialogue ();
			triggered = true;
			this.gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}
}
