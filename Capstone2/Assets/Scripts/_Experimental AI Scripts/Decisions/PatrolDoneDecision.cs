using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Decisions/Patrol Done Decision")]
public class PatrolDoneDecision : Decision {

	public override bool Decide(StateController controller)
	{
		bool isPatrolling = IsDonePatrolling(controller);
//		Debug.Log ("Is Patrol Done: " + isPatrolling);
		return isPatrolling;
	}
	
	bool IsDonePatrolling(StateController controller)
	{
		return controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance;
	}

}
