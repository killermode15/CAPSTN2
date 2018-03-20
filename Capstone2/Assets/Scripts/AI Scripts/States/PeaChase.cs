using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeaChase : State
{

	public float moveSpeed;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		//Debug.Log("PeaCchasing");
		GetComponentInChildren<Animator>().SetBool("Attack", false);
		GetComponentInChildren<Animator>().SetBool("Flying", true);
		transform.LookAt(new Vector3(Manager.Player.transform.position.x, transform.position.y, Manager.Player.transform.position.z));
		Vector3 chaseTargetPos = new Vector3(Manager.Player.transform.position.x, transform.position.y, Manager.Player.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, chaseTargetPos, moveSpeed * Time.deltaTime);
		float distance = Vector3.Distance(transform.localPosition, Manager.Player.transform.localPosition);
		if (distance <= Manager.attackRange || distance >= Manager.DetectionRange)
		{
			return false;
		}
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
