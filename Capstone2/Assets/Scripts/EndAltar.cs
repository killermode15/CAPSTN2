using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAltar : MonoBehaviour {

    public GameObject Camera;
    public GameObject LookAt;
    private bool isActivated;
    private bool canRestart;
    public GameObject PressX;

	// Use this for initialization
	void Start () {
        canRestart = false;
        PressX.SetActive(false);
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
            StartCoroutine(WaitForRestart(10.0f));
        }

        if (canRestart)
        {
            PressX.SetActive(true);
            if (Input.GetButtonDown("cross"))
                SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator WaitForRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("yo");
        canRestart = true;
    }
}
