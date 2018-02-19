using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundedPatrol : State {

	[Tooltip("Lists of patrol points")]
	public List<Transform> PatrolPoints;
	[Tooltip("The currently selected patrol point")]
	public int CurrentPatrolPoint;

	NavMeshAgent agent;

	public override void OnEnable()
	{
		agent = GetComponent<NavMeshAgent> ();
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		base.OnUpdate();
		if(!IsPatrolDone())
		{
			//Movement
			//transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, moveSpeed * Time.deltaTime);
			agent.destination = PatrolPoints[CurrentPatrolPoint].position;
			return true;
		}

		CurrentPatrolPoint++;
		if (CurrentPatrolPoint >= PatrolPoints.Count) {
			CurrentPatrolPoint = 0;
		}

		return false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	bool IsPatrolDone()
	{
		float dist = Vector3.Distance(transform.position, PatrolPoints[CurrentPatrolPoint].position);

		return (dist <= 1);
	}
}
