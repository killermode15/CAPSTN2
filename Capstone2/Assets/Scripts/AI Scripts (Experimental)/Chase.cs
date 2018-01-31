using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State {

	public float moveSpeed;

	public override void OnEnable()
	{
		//Debug.Log (enabled);
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Vector3 chaseTargetPos = new Vector3 (Manager.Player.transform.position.x, 0, Manager.Player.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, chaseTargetPos, moveSpeed * Time.deltaTime);
		float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
		if (distance <= Manager.attackRange || distance >= Manager.DetectionRange) {
			return false;
		}
		return true;
	}

	public override void OnDisable()
	{
		Debug.Log (enabled);
		base.OnDisable();
	}
}
