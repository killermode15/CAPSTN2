using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class EndAltar : MonoBehaviour {

    public GameObject Camera;
    public GameObject LookAt;
    private bool isActivated;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        ///If Altar doesnt require 10 orbs
        /*if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
        {
            GetComponent<AltarObject>().isActivated = true;
        }*/
            //if(Input.GetButtonDown("TouchPad") && !isActivated)
        if (GetComponent<AltarObject>().isActivated && !isActivated)
        {
            Camera.GetComponent<CameraController>().enabled = false;
            Camera.GetComponent<ParticleFollowPath>().Activate();
            isActivated = true;
        }

        if(isActivated)
        {
            Camera.transform.LookAt(LookAt.transform);
        }
    }
}
