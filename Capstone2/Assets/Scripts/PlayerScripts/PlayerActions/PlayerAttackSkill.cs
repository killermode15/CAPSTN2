using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSkill : MonoBehaviour, IPlayerAction
{
	public float Cooldown;
	public float Damage;
	public float Range;
	public GameObject Arm;
	float MeleeTimer;

	// Use this for initialization
	void Start () {
		Arm.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Cooldown >= 0)
			Cooldown -= Time.deltaTime;
		if (Cooldown <= 0) 
			UseAction ();

		if(MeleeTimer >= 0)
			MeleeTimer -= Time.deltaTime;
		if(MeleeTimer <= 0)
			Arm.SetActive (false);
		
	}

	public void UseAction()
	{
		//throw new NotImplementedException();
		if (InputManager.Instance.GetKeyDown(ControllerInput.Attack)) {
			Cooldown = 3;
			Debug.Log ("MELEE HAYA");

			///Raycasts Punch lel
			/*RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), Range)) {
				//sending/applying damage
				//hit.transform.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
				Debug.Log (Range);
			}*/

			///Gameobject Punch
			/// NOTE: I made a Melee script (for the collision of the gameobject) "Melee.cs"
			MeleeTimer = 0.35f;
			Arm.SetActive(true);
		}

	}

	public void UseActionWithElementModifier(Element element)
	{
		throw new NotImplementedException();
	}
}
