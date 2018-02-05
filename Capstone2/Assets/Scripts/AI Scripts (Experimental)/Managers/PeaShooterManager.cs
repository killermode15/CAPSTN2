using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeaShooterManager : StateManager {


	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		ChangeState(GetState("Patrol"));
	}

	// Update is called once per frame
	void Update () {
		StateTransition();
	}

	public override void StateTransition()
	{
		if (CompareToCurrentState (typeof(Patrol))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("Patrol"));
			}
		}
	}

}
