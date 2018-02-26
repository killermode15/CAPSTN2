using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decision", menuName = "AI/Decisions/Dead Decision")]
public class DeadDecision : Decision {

	public override bool Decide (StateController controller)
	{
		bool isDead = IsDead (controller);

		return isDead;
	}

	bool IsDead(StateController controller)
	{
		return controller.bossHealth.HealthChunks <= 0;
	}
}
