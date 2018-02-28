using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decision", menuName = "AI/Decisions/Wait Decision")]
public class WaitDecision : Decision {

	public override bool Decide(StateController controller)
	{
		bool isWaitingDone = Wait(controller);
		if(isWaitingDone)
			controller.navMeshAgent.isStopped = false;
		return isWaitingDone;
	}

	bool Wait(StateController controller)
	{
		return controller.IsDoneWaiting();
	}
}
