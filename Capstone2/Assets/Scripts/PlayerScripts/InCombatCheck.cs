using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCombatCheck : MonoBehaviour {

    public bool inCombat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ///Will true if:
        ///uses LockOn
        ///getsDamaged (by enemy)
        if (GetComponent<LockOn>().currentTarget != null)
        {
            Debug.Log("Play Music");
        }
        if (GetComponent<HP>().damagedByEnemy)
        {
            Debug.Log("Play Music");
        }

        ///Will false if:
        ///releases LockOn after 3 seconds
        ///no enemies within vicinity && dead targets
        
    }


}
