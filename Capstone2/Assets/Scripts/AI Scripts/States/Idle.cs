using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

	public bool IsIdling;
	public float idleTime;

	public override void OnEnable()
	{
		IsIdling = true;
		StartCoroutine(Wait());
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		if (!IsIdling)
		{
			return false;
		}
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	IEnumerator Wait()
	{
		IsIdling = true;
		yield return new WaitForSeconds(idleTime);
		IsIdling = false;
	}

}
