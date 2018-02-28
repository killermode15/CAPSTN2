using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Charge Action")]
public class TrollChargeAction : Action {

	public override void Act(StateController controller)
	{
		Charge(controller);
	}

	void Charge(StateController controller)
	{
		//Do charge here
		controller.attackScript.Charge (controller);
	}
}
