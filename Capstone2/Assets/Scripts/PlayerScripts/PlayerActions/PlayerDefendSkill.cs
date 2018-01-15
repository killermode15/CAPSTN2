using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefendSkill : MonoBehaviour, IPlayerAction {

	public float Cooldown;
	public float Capacity;
	public GameObject Shield;
	float DefenseTimer;

	// Use this for initialization
	void Start () {
		Shield.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Cooldown >= 0)
			Cooldown -= Time.deltaTime;
		if (Cooldown <= 0) 
			UseAction ();

		if(DefenseTimer >= 0)
			DefenseTimer -= Time.deltaTime;
		if(DefenseTimer <= 0)
			Shield.SetActive (false);
		
	}

	public void UseAction()
	{
		//throw new NotImplementedException();
		///Gameobject Shield
		/// NOTE: I made a Shield script (for the collision of the gameobject) "Shield.cs"
		if (InputManager.Instance.GetKeyDown(ControllerInput.Defend))
		{
			Cooldown = 3.0f;
			Debug.Log ("SHIELD vwwooosh");

			DefenseTimer = 1.0f;
			Shield.SetActive(true);
		}
	}

	public void UseActionWithElementModifier(Element element)
	{
		throw new NotImplementedException();
	}
}
