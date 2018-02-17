using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSkill : MonoBehaviour, IPlayerAction
{
	public float dashCD;
	public float DashSpeed;
	public float DashDuration;
	private float currentLerpTime;
	private PlayerController controllerScriptRef;
	private bool canDash;

	// Use this for initialization
	void Start()
	{
		canDash = true;
		controllerScriptRef = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		UseAction();
		if (controllerScriptRef.anim.GetBoolAnimParam("IsRolling"))
		{
			if (controllerScriptRef.anim.GetStateInfo().normalizedTime >= 1f)
			{
				controllerScriptRef.anim.SetBoolAnimParam("IsRolling", false);
			}
		}
	}

	IEnumerator Dash()
	{
		canDash = false;
		yield return new WaitForSeconds(dashCD);
		canDash = true;
	}

	public void UseAction()
	{
		if (InputManager.Instance.GetKeyDown(ControllerInput.Move) && controllerScriptRef.CanMove)
		{
			Debug.Log("DAsh");
			controllerScriptRef.anim.SetBoolAnimParam("IsRolling", true);
			controllerScriptRef.AddForwardVelocity(DashDuration, DashSpeed);
			//StartCoroutine(Dash());
			if (canDash) {
				controllerScriptRef.AddForwardVelocity(controllerScriptRef.anim.GetCurrentAnimationLength(), DashSpeed);
				StartCoroutine (Dash ());
			}
		}
	}

	public void UseActionWithElementModifier(Element element)
	{
		throw new NotImplementedException();
	}
}
