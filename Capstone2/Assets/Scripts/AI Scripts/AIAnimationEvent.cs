using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerDamage()
    {
		Debug.Log(name);
        GetComponentInParent<StateManager>().Player.GetComponent<HP>().RemoveHealth(GetComponentInParent<StateManager>().damage );
    }
}
