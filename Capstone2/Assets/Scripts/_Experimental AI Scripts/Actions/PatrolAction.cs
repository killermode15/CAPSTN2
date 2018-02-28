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
		if (!controller.navMeshAgent.pathPending) {
			if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance) {
				if (!controller.navMeshAgent.hasPath || controller.navMeshAgent.velocity.sqrMagnitude == 0f) {
					controller.navMeshAgent.speed = controller.Stats.MoveSpeed;
					controller.navMeshAgent.SetDestination (controller.Destination.position);
					controller.nextPatrolPoint++;
					controller.navMeshAgent.isStopped = false;
					//controller.animator.SetBool ("Patrol", true);

					if (controller.nextPatrolPoint >= controller.patrolPoints.Count)
						controller.nextPatrolPoint = 0;
				}
			}
		}

		if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance) {

			//controller.animator.SetBool ("Patrol", false);
		}
	}
}