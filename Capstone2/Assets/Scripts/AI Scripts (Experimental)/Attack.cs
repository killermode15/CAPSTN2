using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

	public override void OnEnable()
	{
		Debug.Log("starting attack:");
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Charge ();

		float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
		if (distance >= Manager.attackRange) {
			return false;
		}
		return true;
	}

	void Charge(){
		//pause for a while
		Debug.Log("pasuing:");
		//swiftly move at player

		//....?
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.5f);
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
