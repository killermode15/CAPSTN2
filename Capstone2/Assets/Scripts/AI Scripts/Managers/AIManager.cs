using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIManager : StateManager {

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		ChangeState(GetState("GroundedPatrol"));
		PauseManager.Instance.addPausable (this);
	}

	void OnDisable(){
		PauseManager.Instance.removePausable (this);
	}

	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			CheckIfPlayerInRange ();
			StateTransition ();
		}
	}

	public override void StateTransition()
	{
		base.StateTransition ();
		//if(!GetComponent<AbsorbableCorruption>().HasEnergyLeft())
		//{
		//	ChangeState(GetState("Dead"));
		//	CurrentState.OnUpdate();
		//}

		//Compare the current state to check if the current state is idle
		if (CompareToCurrentState (typeof(Idle))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				//Transition to Patrol
				ChangeState (GetState ("GroundedPatrol"));
			}
		} else if (CompareToCurrentState (typeof(GroundedPatrol))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				//Transition to Idle
				ChangeState (GetState ("Idle"));
			}
		} else if (CompareToCurrentState (typeof(Chase))) {
			if (!CurrentState.OnUpdate ()) {
				if (playerDistance >= DetectionRange) {
					//Debug.Log ("out of range!");
					ChangeState (GetState ("Idle"));
				} else if (playerDistance <= attackRange) {
					//Debug.Log ("in range for attack!");
					ChangeState (GetState ("Attack"));
				}
			}
		} else if (CompareToCurrentState (typeof(Attack))) {
			if (!CurrentState.OnUpdate ()) {
				if (playerDistance >= attackRange) {
					ChangeState (GetState ("Chase"));
				}
			}
		}
		else
			CurrentState.OnUpdate ();
	}

	public override void CheckIfPlayerInRange(){
		playerDistance = Vector3.Distance (Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange) {
			if(!CompareToCurrentState(typeof(Chase)) && !CompareToCurrentState(typeof(Attack))&& !CompareToCurrentState(typeof(StunnedState)))
				ChangeState (GetState("Chase"));
		}
	}

	public override void Pause(){
		isPaused = true;
	}

	public override void UnPause(){
		isPaused = false;
	}
}
