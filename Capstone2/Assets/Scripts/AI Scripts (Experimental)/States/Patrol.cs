using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State {

	[Tooltip("Lists of patrol points")]
	public List<Transform> PatrolPoints;
	[Tooltip("The currently selected patrol point")]
	public int CurrentPatrolPoint;
	[Tooltip("Offset between the target and the current position")]
	public float GoalDistanceCompensation;
	public float moveSpeed;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		base.OnUpdate();
		if(!IsPatrolDone())
		{
			//Movement
			transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, moveSpeed * Time.deltaTime);
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

		return (dist <= 1 + GoalDistanceCompensation);
	}
}
