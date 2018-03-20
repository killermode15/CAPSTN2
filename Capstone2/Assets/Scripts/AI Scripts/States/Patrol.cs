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
		//Debug.Log("Patrolling");
		base.OnUpdate();
		GetComponentInChildren<Animator>().SetBool("Attack", false);
		GetComponentInChildren<Animator>().SetBool("Flying", true);
		if (!IsPatrolDone())
		{
			//Movement
			transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, moveSpeed * Time.deltaTime);
			transform.LookAt(new Vector3(PatrolPoints[CurrentPatrolPoint].position.x, transform.position.y, PatrolPoints[CurrentPatrolPoint].position.z));
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
