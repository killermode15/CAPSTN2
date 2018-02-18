﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTerrainSkill : MonoBehaviour {

	public UseSkill UseSkillRef;
	public bool earthSkillUsed;

	// Use this for initialization
	void OnEnable () {
		earthSkillUsed = false;
		UseSkillRef.onSkillUse += CheckIfTerrainIsUsed;
	}

	public void CheckIfTerrainIsUsed(Element element)
	{
		Debug.Log(earthSkillUsed);
		if(element.GetType() == typeof(EarthElement))
		{
			//check if skill used
			earthSkillUsed = true;
			Debug.Log(earthSkillUsed);
		}
	}

	// Update is called once per frame
	void OnDisable () {
		UseSkillRef.onSkillUse -= CheckIfTerrainIsUsed;
	}
}
