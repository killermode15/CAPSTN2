using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour {

	public GameObject Crosshair;
	public GameObject currentTarget;
	[HideInInspector] public List<GameObject> allEnemies = new List<GameObject> ();
	[HideInInspector] public List<GameObject> visibleEnemies = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		//FindAllEnemies ();
		//allEnemies = GameObject.FindObjectsOfType<Absorbable>().ToList();

		Crosshair = Instantiate (Crosshair);
		Crosshair.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfInCombat ();
	}

	void CheckIfInCombat(){
		if (Input.GetButton ("Jump")) {
			CheckForEnemiesInRange ();
			//FindAllEnemies ();
		} else {
			Crosshair.SetActive (false);
		}
	}

	void PickTarget(){
		// if nothing to switch to, set Crosshair.SetActive(false);
	}

	void FindAllEnemies(){
		/*State[] allEnemies = FindObjectsOfType (typeof(State)) as State[];
		foreach (State Enemy in allEnemies) {
			//visibleEnemies.Add(Enemy);
			Debug.Log(Enemy.name);
			CheckForEnemiesInRange (Enemy);
		}*/
	}

	void CheckForEnemiesInRange(/*GameObject enemy*/){
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(currentTarget.transform.position);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
		if (onScreen) {
			CrosshairLock ();
			//visibleEnemies.Add(enemy);
		} else {
			//Crosshair.SetActive (false);
			PickTarget();
		}
	}

	void CrosshairLock(){
		//spawns crosshair
		Crosshair.SetActive(true);
		Crosshair.transform.position = currentTarget.transform.position;
	}
}
