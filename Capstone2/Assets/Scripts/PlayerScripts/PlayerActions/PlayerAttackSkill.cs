using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSkill : MonoBehaviour, IPlayerAction
{
	public PlayerAnimation Animation;
	public int NoOfAttackAnimation;
	public float Cooldown;
	public float Damage;
	public float Range;

	private float meleeTimer;
	private int attackIndex;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Cooldown >= 0)
			Cooldown -= Time.deltaTime;
		if (Cooldown <= 0) 
			UseAction ();

		if(meleeTimer >= 0)
			meleeTimer -= Time.deltaTime;
		
	}

	public void UseAction()
	{
		Animation.PlayAnimation(PlayerAnimationState.Attack);
		if (InputManager.Instance.GetKeyDown(ControllerInput.Attack)) {
			Debug.Log ("MELEE HAYA");
			

			///Raycasts Punch lel
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, Range)) {
				//sending/applying damage
				//hit.transform.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
				Debug.Log (hit.collider.tag);
			}

			///Gameobject Punch
			/// NOTE: I made a Melee script (for the collision of the gameobject) "Melee.cs"
			meleeTimer = 0.35f;
			//Arm.SetActive(true);
		}

	}

	public void UseActionWithElementModifier(Element element)
	{
		throw new NotImplementedException();
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, transform.position + (transform.forward * Range));
	}
}
