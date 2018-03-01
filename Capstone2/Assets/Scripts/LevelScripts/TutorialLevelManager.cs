using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour {

	public GameObject EnemySnake;
	public GameObject Altar;
	public GameObject Portal;

	private Transform FirstSetDialogue;
	private Transform SecondSetDialogue;
	private Transform WindEnable;
	private Transform EarthEnable;
	private Transform WaterEnable;
	//private Transform ThirdSetDialogue;
	public GameObject Pause;

	public UseSkill SkillElements;

	// Use this for initialization
	void Start () {
		Portal.SetActive (false);
		FirstSetDialogue = this.gameObject.transform.GetChild (0);
		SecondSetDialogue = this.gameObject.transform.GetChild (1);
		//ThirdSetDialogue = this.gameObject.transform.GetChild (2);
		WindEnable = this.gameObject.transform.GetChild (4);
		EarthEnable = this.gameObject.transform.GetChild (5);
		WaterEnable = this.gameObject.transform.GetChild (6);

		SecondSetDialogue.gameObject.SetActive (false);
		//ThirdSetDialogue.gameObject.SetActive (false);
		SkillElements.GetElement(typeof(WindElement)).IsElementUnlocked = false;
		SkillElements.GetElement(typeof(EarthElement)).IsElementUnlocked = false;
		SkillElements.GetElement(typeof(WaterElement)).IsElementUnlocked = false;
		SkillElements.GetElement(typeof(FireElement)).IsElementUnlocked = false;
	}
	
	// Update is called once per frame
	void Update () {
		DialogueTriggers ();
		AltarCheck();
		if (GetComponent<CheckForTerrainSkill>().earthSkillUsed && !SecondSetDialogue.GetComponent<DialogueTrigger> ().triggered) {
			SecondSetDialogue.gameObject.SetActive (true);
			SecondSetDialogue.GetComponent<DialogueTrigger> ().triggered = true;
			PauseManager.Instance.Pause ();
		}
	}

	void AltarCheck(){
		if (Altar.GetComponent<AltarObject> ().IsFull)
			Portal.SetActive (true);
	}

	void DialogueTriggers(){
		if (FirstSetDialogue.gameObject.GetComponent<DialogueTrigger> ().triggered) {
			PauseManager.Instance.Pause ();
			FirstSetDialogue.gameObject.GetComponent<DialogueTrigger> ().triggered = false;
		}
		if (WindEnable.gameObject.GetComponent<DialogueTrigger> ().triggered) {
			SkillElements.GetElement(typeof(WindElement)).IsElementUnlocked = true;
		}
		if (EarthEnable.gameObject.GetComponent<DialogueTrigger> ().triggered) {
			SkillElements.GetElement(typeof(EarthElement)).IsElementUnlocked = true;
		}
		if (WaterEnable.gameObject.GetComponent<DialogueTrigger> ().triggered) {
			SkillElements.GetElement(typeof(WaterElement)).IsElementUnlocked = true;
		}
	}
}
