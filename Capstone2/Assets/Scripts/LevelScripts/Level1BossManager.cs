using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BossManager : MonoBehaviour {

	public GameObject EnterTrigger;
	public GameObject Closing;
	public GameObject TrollAITrigger;
	public GameObject Stalactites;
	public GameObject Troll;
	public Camera camera;
	public GameObject exitPortal;
	public bool startFall;

	// Use this for initialization
	void Start () {
		Closing.SetActive (false);
		exitPortal.SetActive (false);
		Troll.GetComponent<StateController> ().isAIActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		TriggerChecks ();
		if(Troll.GetComponent<BossHealth>().HealthChunks <= 0)
			exitPortal.SetActive (true);
	}

	void TriggerChecks(){
		if (EnterTrigger.GetComponent<TriggerCheck> ().isTriggered) 
			Closing.SetActive (true);
		if (TrollAITrigger.GetComponent<TriggerCheck> ().isTriggered) {
			Troll.GetComponent<StateController> ().isAIActive = true;
			camera.GetComponent<CameraFollow> ().Offset.z = -4.5f;
		}
	}
}
