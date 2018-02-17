using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour {

	private Transform FirstSetDialogue;
	private Transform SecondSetDialogue;
	//private Transform ThirdSetDialogue;
	public GameObject Pause;

	// Use this for initialization
	void Start () {
		FirstSetDialogue = this.gameObject.transform.GetChild (0);
		SecondSetDialogue = this.gameObject.transform.GetChild (1);
		//ThirdSetDialogue = this.gameObject.transform.GetChild (2);

		SecondSetDialogue.gameObject.SetActive (false);
		//ThirdSetDialogue.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (/*when player walls out the snake successfully*/ Input.GetButton("Jump")) {
			SecondSetDialogue.gameObject.SetActive (true);
			PauseManager.Instance.Pause ();
		}

		if (FirstSetDialogue.gameObject.GetComponent<DialogueTrigger> ().triggered) {
			PauseManager.Instance.Pause ();
			FirstSetDialogue.gameObject.GetComponent<DialogueTrigger> ().triggered = false;
		}
	}
}
