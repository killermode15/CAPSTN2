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
	public override void Start () {
		base.Start ();
		ChangeState(GetState("Patrol"));
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
		if (CompareToCurrentState (typeof(Patrol))) {
			//If the current state is not updating
			if (!CurrentState.OnUpdate ()) {
				ChangeState (GetState ("Patrol"));
			}
		}
	}

	public override void Pause(){
		isPaused = true;
	}

	public override void UnPause(){
		isPaused = false;
	}
}
