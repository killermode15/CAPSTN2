using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour {

	private Transform FirstSetDialogue;
	private Transform SecondSetDialogue;
	bool enable;

	// Use this for initialization
	void Start () {
		FirstSetDialogue = this.gameObject.transform.GetChild(0);
		SecondSetDialogue = this.gameObject.transform.GetChild (1);

		enable = false;
		SecondSetDialogue.gameObject.SetActive (enable);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (/*when player traps snake successfully) {
			enable = true;
		}*/
		if (FirstSetDialogue.GetComponent<DialogueTrigger>().triggered)
		{
			FirstSetDialogue.GetComponent<DialogueTrigger>().triggered = false;
			SecondSetDialogue.gameObject.SetActive(enable);
		}
	}
}
