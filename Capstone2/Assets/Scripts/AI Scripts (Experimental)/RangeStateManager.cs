using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeStateManager : StateManager {

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
	}

	// Use this for initialization
	void Start () {
		PossibleStates = GetComponents<State>().ToList();
		ChangeState(GetState("Idle"));
	}

	// Update is called once per frame
	void Update () {
		CheckIfPlayerInRange ();
		StateTransition();
	}

	public override void StateTransition()
	{
		if(CompareToCurrentState(typeof(RangeAttack)))
		{
			//If the current state is not updating
			if (!CurrentState.OnUpdate())
			{
				ChangeState(GetState("Idle"));
			}
		}
	}

	public override void CheckIfPlayerInRange(){
		playerDistance = Vector3.Distance (Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange) {
			if(!CompareToCurrentState(typeof(RangeAttack)))
				ChangeState (GetState("RangeAttack"));
		}
	}

}
