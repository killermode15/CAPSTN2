using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State {

	public float moveSpeed;
	NavMeshAgent agent;

	public override void OnEnable()
	{
		agent = GetComponent<NavMeshAgent> ();
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Vector3 chaseTargetPos = new Vector3 (Manager.Player.transform.position.x, transform.position.y, Manager.Player.transform.position.z);
		//transform.position = Vector3.MoveTowards(transform.position, chaseTargetPos, moveSpeed * Time.deltaTime);
		agent.destination = chaseTargetPos;
		float distance = Vector3.Distance (transform.localPosition, Manager.Player.transform.localPosition);
		if (distance <= Manager.attackRange || distance >= Manager.DetectionRange) {
			return false;
		}
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
