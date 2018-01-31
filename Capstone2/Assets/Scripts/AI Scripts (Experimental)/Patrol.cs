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
			return true;
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
