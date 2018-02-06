using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSkill : MonoBehaviour, IPlayerAction
{

	public float DashSpeed;
	public float DashDuration;
	private float currentLerpTime;
	private PlayerController controllerScriptRef;

	// Use this for initialization
	void Start()
	{
		controllerScriptRef = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		UseAction();
	}

	IEnumerator Dash()
	{
		float lerpPerc = 0;
		currentLerpTime = 0;
		do
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > DashDuration)
				currentLerpTime = DashDuration;

			lerpPerc = currentLerpTime / DashDuration;
			//transform.position += Vector3.right * Input.GetAxisRaw("LeftStickX") * 
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (DashSpeed * Input.GetAxis("Horizontal")), transform.position.y, transform.position.z), lerpPerc);
			yield return new WaitForEndOfFrame();
		} while (lerpPerc < 1);
	}

	public void UseAction()
	{
		if (InputManager.Instance.GetKeyDown(ControllerInput.Move) && controllerScriptRef.CanMove)
			controllerScriptRef.AddForwardVelocity(DashDuration);
			//StartCoroutine(Dash());
	}

	public void UseActionWithElementModifier(Element element)
	{
		throw new NotImplementedException();
	}
}
