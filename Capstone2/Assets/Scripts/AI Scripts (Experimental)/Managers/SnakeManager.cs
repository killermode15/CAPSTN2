using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AbsorbableCorruption))]
public class SnakeManager : StateManager {

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

	// Use this for initialization
	void Start () {
		base.Start ();
		ChangeState(GetState("Idle"));
	}

	// Update is called once per frame
	void Update () {
		CheckIfPlayerInRange ();
		StateTransition();
	}

	public virtual void StateTransition()
	{
		if(!GetComponent<AbsorbableCorruption>().HasEnergyLeft())
		{
			ChangeState(GetState("Dead"));
			CurrentState.OnUpdate();
		}

		//Compare the current state to check if the current state is idle
		if (CompareToCurrentState (typeof(Idle))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				//Transition to Patrol
				ChangeState (GetState ("Patrol"));
			}
		} else if (CompareToCurrentState (typeof(Patrol))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				//Transition to Idle
				ChangeState (GetState ("Idle"));
			}
		} else if (CompareToCurrentState (typeof(Chase))) {
			if (!CurrentState.OnUpdate ()) {
				if (playerDistance >= DetectionRange) {
					Debug.Log ("out of range!");
					ChangeState (GetState ("Idle"));
				} else if (playerDistance <= attackRange) {
					Debug.Log ("in range for attack!");
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

	public void ChangeState(State newState)
	{
		if (CurrentState)
		{
			CurrentState.enabled = false;
		}
		CurrentState = newState;
		newState.enabled = true;
	}

	public State GetState(string name)
	{
		return PossibleStates.Find(x => x.GetType().Name.ToLower() == name.ToLower());
	}

	public bool CompareToCurrentState(System.Type stateType)
	{
		if (CurrentState)
			return CurrentState.GetType() == stateType;
		else
			return false;
	}

	public virtual void CheckIfPlayerInRange(){
		playerDistance = Vector3.Distance (Player.transform.position, transform.position);
		if (playerDistance <= DetectionRange) {
			if(!CompareToCurrentState(typeof(Chase)) && !CompareToCurrentState(typeof(Attack)))
				ChangeState (GetState("Chase"));
		}
	}
}
