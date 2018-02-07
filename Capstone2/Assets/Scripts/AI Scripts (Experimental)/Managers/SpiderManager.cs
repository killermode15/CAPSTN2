using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpiderManager : StateManager {


	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, DetectionRange);
	}

	void Start(){
		base.Start ();
		ChangeState (GetState ("Idle"));
	}

	void Update(){
		CheckIfPlayerInRange ();
		StateTransition ();
	}

	public override void StateTransition(){
		if (CompareToCurrentState (typeof(RangeAttack))) {
			//if the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("Idle"));
			}
		}
	}

	public override void CheckIfPlayerInRange(){
		playerDistance = Vector3.Distance (Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange) {
			if (!CompareToCurrentState (typeof(RangeAttack))) {
				ChangeState (GetState ("RangeAttack"));
			}
		}
	}
}
