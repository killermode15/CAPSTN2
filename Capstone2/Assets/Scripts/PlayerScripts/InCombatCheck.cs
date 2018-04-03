﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InCombatCheck : MonoBehaviour {

	public AudioMixerSnapshot InCombatSnapshot;
	public AudioMixerSnapshot NormalBGM;
    public bool inCombat;

	private bool isCombatBGMPlaying;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("In Combat is: " + inCombat);
        ///Will true if:
        ///uses LockOn
        ///getsDamaged (by enemy)
        if (GetComponent<LockOn>().currentTarget != null)
        {
            SetInCombat();

            ///Will false if:
            ///no enemies within vicinity && dead targets
            if (GetComponent<LockOn>().currentTarget.GetComponent<StateManager>().HP <= 0 && GetComponent<LockOn>().visibleEnemies.Count == 0)
            {
                StartCoroutine(WaitBeforeExitCombat(3.0f));
            }
        }
        if (GetComponent<HP>().damagedByEnemy)
        {
            SetInCombat();
        }

        ///Will false if:
        ///no enemies within vicinity && releases LockOn after 3 seconds
        if (GetComponent<LockOn>().currentTarget == null && GetComponent<LockOn>().visibleEnemies.Count == 0)
        {
            StartCoroutine(WaitBeforeExitCombat(1.0f));
        }
    }

    public void SetInCombat()
    {
        inCombat = true;
		StopAllCoroutines();

		// This is for bgm changing
		if (!isCombatBGMPlaying)
		{
			InCombatSnapshot.TransitionTo(4.0f);
		}
    }

    IEnumerator WaitBeforeExitCombat(float time)
    {
        yield return new WaitForSeconds(time);
        inCombat = false;
        GetComponent<HP>().damagedByEnemy = false;
		StopAllCoroutines();

		// This is for bgm changing
		NormalBGM.TransitionTo(4.0f);
		isCombatBGMPlaying = false;
	}

}
