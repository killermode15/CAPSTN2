using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour {

	public bool bossLevelClean;

	public List <GameObject> Altars = new List<GameObject>();
	public List <GameObject> CorruptedLevel = new List<GameObject>();
	public List <GameObject> CleanLevel = new List<GameObject>();

	// Use this for initialization
	void Start () {
		bossLevelClean = false;

		//sets everything to corrupted
		for (int i = 0; i < CorruptedLevel.Count; i++) {
			CorruptedLevel [i].SetActive (true);
			CleanLevel [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		FirstAltarActivate ();
		SecondAltarActivate ();
		ThirdAltarActivate ();
		BossAltarActivate ();
	}

	public void FirstAltarActivate() {
		if (Altars [0].GetComponent<AltarObject> ().isActivated) {
			CorruptedLevel [0].SetActive (false);
			CleanLevel [0].SetActive (true);
		}
	}

	public void SecondAltarActivate() {
		if (Altars [1].GetComponent<AltarObject> ().isActivated) {
			CorruptedLevel [1].SetActive (false);
			CleanLevel [1].SetActive (true);
		}
	}

	public void ThirdAltarActivate() {
		if (Altars [2].GetComponent<AltarObject> ().isActivated) {
			CorruptedLevel [2].SetActive (false);
			CleanLevel [2].SetActive (true);
		}
	}

	public void BossAltarActivate() {
		if (bossLevelClean) {
			CorruptedLevel [3].SetActive (false);
			CleanLevel [3].SetActive (true);
		}
	}
}
