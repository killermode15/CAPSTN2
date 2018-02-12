using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour {

	private Transform SecondSetDialogue;
	bool enable;

	// Use this for initialization
	void Start () {
		SecondSetDialogue = this.gameObject.transform.GetChild (1);

		enable = false;
		SecondSetDialogue.gameObject.SetActive (enable);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (/*when player traps snake successfully) {
			enable = true;
		}*/
		SecondSetDialogue.gameObject.SetActive (enable);
	}
}
