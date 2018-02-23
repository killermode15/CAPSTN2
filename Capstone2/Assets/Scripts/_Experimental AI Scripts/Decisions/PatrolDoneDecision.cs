using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Decisions/Patrol Done Decision")]
public class PatrolDoneDecision : Decision {

	public override bool Decide(StateController controller)
	{
		bool isPatrolling = IsDonePatrolling(controller);
		return isPatrolling;
	}
	
	bool IsDonePatrolling(StateController controller)
	{
		return Vector3.Distance(controller.navMeshAgent.destination, controller.transform.position) <= controller.navMeshAgent.stoppingDistance;
	}

}
