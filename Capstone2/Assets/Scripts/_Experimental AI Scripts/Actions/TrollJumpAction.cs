using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Jump Action")]
public class TrollJumpAction : Action {

	public override void Act(StateController controller)
	{
		JumpAttack(controller);
	}

	void JumpAttack(StateController controller)
	{
		controller.attackScript.JumpAttack (controller);
	}
}
