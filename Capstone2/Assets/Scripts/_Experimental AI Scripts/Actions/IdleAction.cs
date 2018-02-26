using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Idle Action")]
public class IdleAction : Action {

	public override void Act (StateController controller)
	{
		Idle (controller);
	}

	void Idle(StateController controller)
	{
		controller.navMeshAgent.isStopped = true;
	}
}
