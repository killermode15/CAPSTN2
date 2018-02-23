using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Patrol Action")]
public class PatrolAction : Action
{
	public override void Act(StateController controller)
	{
		Patrol(controller);
	}

	void Patrol(StateController controller)
	{
		Debug.Log("I am patrolling");
		Debug.Log(controller.navMeshAgent.isStopped);
		if (!controller.navMeshAgent.pathPending)
		{
			if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
			{
				if (!controller.navMeshAgent.hasPath || controller.navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					Debug.Log("Setting new destination");
					controller.navMeshAgent.SetDestination(controller.patrolPoints[controller.nextPatrolPoint].position);
					controller.navMeshAgent.Resume();
					controller.nextPatrolPoint++;
					if (controller.nextPatrolPoint >= controller.patrolPoints.Count)
						controller.nextPatrolPoint = 0;
				}
			}
		}
	}
}
